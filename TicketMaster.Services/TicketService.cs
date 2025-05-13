using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Services
{
    public interface ITicketService
    {
        Task<List<TicketGetDTO>> GetTicketsAsync();
        Task<List<TicketGetDTO>> GetTicketsByScreeningIdAsync(int screeningId);
        Task<TicketGetDTO> GetTicketByIdAsync(int id);
        Task PutTicketAsync(int ticketId, TicketPutDTO dto);
        Task PostTicketAsync(TicketPostDTO dto);
        Task DeleteTicketAsync(int id);
        Task<bool> Validate(int ticketId);
    }
    public class TicketService : ITicketService
    {
        private UnitOfWork unitOfWork;
        private IMapper mapper;
        private AppDbContext context;
        public TicketService(UnitOfWork _unitOfWork, IMapper _mapper, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            context = _context;
        }
        public async Task DeleteTicketAsync(int id)
        {
            await unitOfWork.TicketRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<TicketGetDTO>> GetTicketsByScreeningIdAsync(int screeningId)
        {
            return mapper.Map<List<TicketGetDTO>>(await context.Tickets.Where(t => t.ScreeningId == screeningId).ToListAsync());
        }

        public async Task<TicketGetDTO> GetTicketByIdAsync(int id)
        {
            var ticket = await context.Tickets
                .Where(ticket => ticket.Id == id)
                .Include(f => f.Purchase)
                .Include(f => f.Screening)
                .ThenInclude(f => f.Film)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket (id: {id}) not found");
            }

            return mapper.Map<TicketGetDTO>(ticket);
        }

        public async Task<List<TicketGetDTO>> GetTicketsAsync()
        {
            var tickets = await context.Tickets
                .Include(f => f.Purchase)
                .Include(f => f.Screening)
                .ThenInclude(f => f.Film)
                .ToListAsync();
            return mapper.Map<List<TicketGetDTO>>(tickets);
        }

        public async Task PostTicketAsync(TicketPostDTO dto)
        {
            if (dto.PurchaseId != null)
            {
                Purchase? purchase = await unitOfWork.PurchaseRepository.GetByIdAsync(dto.PurchaseId.Value);
                if (purchase == null)
                {
                    throw new ArgumentException($"Purchase does not exists");
                }
            }

            Screening? screening = await unitOfWork.ScreeningRepository.GetByIdAsync(dto.ScreeningId);
            if (screening == null)
            {
                throw new ArgumentException($"Screening does not exists");
            }
            else
            {
                Room room = await unitOfWork.RoomRepository.GetByIdAsync(screening.RoomId);
                if (dto.SeatRow > room.MaxSeatRow || dto.SeatRow < 0 || dto.SeatColumn > room.MaxSeatColumn || dto.SeatColumn < 0)
                {
                    throw new ArgumentException($"Incorrect seat assignment");
                }
            }
            Ticket newTicket = mapper.Map<Ticket>(dto);
            await unitOfWork.TicketRepository.InsertAsync(newTicket);
            await unitOfWork.SaveAsync();
        }

        public async Task PutTicketAsync(int ticketId, TicketPutDTO dto)
        {
            Ticket? ticket = await unitOfWork.TicketRepository.GetByIdAsync(ticketId);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket (id: {ticketId}) not found");
            }

            mapper.Map(dto, ticket);
            Screening? screening = await unitOfWork.ScreeningRepository.GetByIdAsync(@ticket.ScreeningId);
            if (screening == null)
            {
                throw new ArgumentException($"Screening does not exists");
            }
            else
            {
                Room room = await unitOfWork.RoomRepository.GetByIdAsync(screening.RoomId);
                if (dto.SeatRow > room.MaxSeatRow || dto.SeatRow < 0 || ticket.SeatColumn > room.MaxSeatColumn || ticket.SeatColumn < 0)
                {
                    throw new ArgumentException($"Incorrect seat assignment");
                }
            }

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(ticketId))
                {
                    throw new KeyNotFoundException($"Ticket (id: {ticketId}) not found");
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task<bool> Validate(int ticketId)
        {
            Ticket? ticket = await unitOfWork.TicketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket does not exists");
            }
            if (ticket.IsValidated)
            {
                throw new ArgumentException("This ticket was already validated");
            }
            ticket.IsValidated = true;
            await unitOfWork.SaveAsync();
            return true;
        }

        private bool TicketExists(int id)
        {
            return unitOfWork.TicketRepository.GetByIdAsync(id) != null;
        }
    }
}
