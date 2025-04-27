using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.ScreeningDTOs;

namespace TicketMaster.Services
{
    public interface IScreeningService 
    {
        Task<List<ScreeningGetDTO>> GetScreeningsAsync();
        Task<ScreeningGetDTO> GetScreeningByIdAsync(int id);
        Task PutScreeningAsync(int screeningId, ScreeningPutDTO dto);
        Task PostScreeningAsync(ScreeningPostDTO dto);
        Task DeleteScreeningAsync(int id);
    }
    public class ScreeningService : IScreeningService
    {
        private UnitOfWork unitOfWork;
        private IMapper mapper;
        
        public ScreeningService(UnitOfWork _unitOfWork, IMapper _mapper) 
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;

        }
        public async Task DeleteScreeningAsync(int id)
        {
            await unitOfWork.ScreeningRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();
        }

        public async Task<ScreeningGetDTO> GetScreeningByIdAsync(int id)
        {
            var screening = await unitOfWork.ScreeningRepository.GetByIdAsync(id);

            if (screening == null)
            {
                throw new KeyNotFoundException($"Screening (id: {id}) not found");
            }

            await unitOfWork.ScreeningRepository.GetByIdAsync(id, includedReferences: ["Film"]);
            await unitOfWork.ScreeningRepository.GetByIdAsync(id, includedCollections: ["Tickets"]);

            return mapper.Map<ScreeningGetDTO>(screening);
        }

        public async Task<List<ScreeningGetDTO>> GetScreeningsAsync()
        {
            var screenings = await unitOfWork.ScreeningRepository.GetAsync(
                includedProperties: ["Film", "Tickets"]
                );
            return mapper.Map<List<ScreeningGetDTO>>(screenings);
        }

        public async Task PostScreeningAsync(ScreeningPostDTO dto)
        {
            Film? film = await unitOfWork.FilmRepository.GetByIdAsync(dto.FilmId);
            if (film == null)
            {
                throw new ArgumentException($"Film does not exists");
            }
            Room? room = await unitOfWork.RoomRepository.GetByIdAsync(dto.RoomId);
            if (room == null)
            {
                throw new ArgumentException($"Room does not exists");
            }
            if (dto.Date < DateTime.Now)
            {
                throw new ArgumentException($"Screening cannot be in the past");
            }

            Screening newScreening = mapper.Map<Screening>(dto);

            await unitOfWork.ScreeningRepository.InsertAsync(newScreening);
            await unitOfWork.SaveAsync();
        }

        public async Task PutScreeningAsync(int screeningId, ScreeningPutDTO dto)
        {
            Screening? screening = await unitOfWork.ScreeningRepository.GetByIdAsync(screeningId);

            if (screening == null)
            {
                throw new KeyNotFoundException($"Screening (id: {screeningId}) not found");
            }

            mapper.Map(dto, screening);

            Film? film = await unitOfWork.FilmRepository.GetByIdAsync(screening.FilmId);
            if (film == null)
            {
                throw new ArgumentException($"Film does not exists");
            }
            Room? room = await unitOfWork.RoomRepository.GetByIdAsync(screening.RoomId);
            if (room == null)
            {
                throw new ArgumentException($"Room does not exists");
            }
            if (screening.Date < DateTime.Now)
            {
                throw new ArgumentException($"Screening cannot be in the past");
            }

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ScreeningExists(screeningId))
                {
                    throw new KeyNotFoundException($"Screening (id: {screeningId}) not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<bool> ScreeningExists(int id)
        {
            return await unitOfWork.ScreeningRepository.GetByIdAsync(id) != null;
        }
    }
}
