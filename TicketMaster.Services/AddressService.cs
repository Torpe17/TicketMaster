using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs.UserDTOs;
using System.Net;
using TicketMaster.Services.DTOs.AddressDTOs;

namespace TicketMaster.Services
{
    public interface IAddressService
    {
        Task<AddressGetDTO> GetAddressByUserIdAsync(int userId);
        Task<AddressGetDTO> CreateAddressAsync(int userId, AddressPostDTO dto);
        Task<AddressGetDTO> UpdateAddressAsync(int userId, AddressPutDTO dto);
        Task DeleteAddressByUserIdAsync(int userId);
    }
    public class AddressService : IAddressService
    {
        private UnitOfWork _unitOfWork;
        private AppDbContext _context;
        private IMapper _mapper;

        public AddressService(UnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public async Task<AddressGetDTO> CreateAddressAsync(int userId, AddressPostDTO dto)
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(userId, includedReferences: ["Address"]);
            if (user == null)
            {
                throw new KeyNotFoundException($"User (id: {userId}) not found");
            }

            if (user.Address != null)
            {
                throw new ArgumentException($"Only one address is allowed for a user");
            }
            Address newAddress = _mapper.Map<Address>(dto);
            newAddress.UserId = userId;

            await _unitOfWork.AddressRepository.InsertAsync(newAddress);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AddressGetDTO>(newAddress);
        }

        public async Task DeleteAddressByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, includedReferences: ["Address"]);

            if (user == null) throw new KeyNotFoundException("User not found");
            if (user.Address == null) throw new KeyNotFoundException("This user doesn't have an address");

            await _unitOfWork.AddressRepository.DeleteByIdAsync(user.Address.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<AddressGetDTO> GetAddressByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, includedReferences: ["Address"]);

            if (user == null) throw new KeyNotFoundException("This user doesn't exists");

            if (user.Address == null) throw new KeyNotFoundException("This user doesn't have an address");

            return _mapper.Map<AddressGetDTO>(user.Address);
        }

        public async Task<AddressGetDTO> UpdateAddressAsync(int userId, AddressPutDTO dto)
        {
            await _unitOfWork.UserRepository.GetByIdAsync(userId, includedCollections: ["Roles"]);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, includedReferences: ["Address"]);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            if (user.Address == null)
            {
                throw new InvalidDataException("User doesn't have address yet.");
            }
            _mapper.Map(dto, user.Address);

            await _context.SaveChangesAsync();

            return _mapper.Map<AddressGetDTO>(user.Address);
        }
    }
}
