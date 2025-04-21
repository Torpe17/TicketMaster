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
        Task<AddressGetDTO> UpdateAddressByIdAsync(int userId, int addressId, AddressPutDTO UserDTO);
        Task<AddressGetDTO> UpdateAddressAsync(int userId, AddressPutDTO addressDto);
        Task DeleteAddressAsync(int id);
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

            await _unitOfWork.AddressRepository.InsertAsync(newAddress);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AddressGetDTO>(newAddress);
        }

        public async Task DeleteAddressAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AddressGetDTO> GetAddressByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AddressGetDTO> UpdateAddressAsync(int userId, AddressPutDTO addressDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AddressGetDTO> UpdateAddressByIdAsync(int userId, int addressId, AddressPutDTO UserDTO)
        {
            throw new NotImplementedException();
        }
    }
}
