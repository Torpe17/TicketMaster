using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs.PurchaseDTOs;

namespace TicketMaster.Services
{
    public interface IPurchaseService
    {
        Task<List<PurchaseGetDTO>> GetPurchasesAsync();
        Task<List<PurchaseGetByIdDTO>> GetPurchasesByUserIdAsync(int userId);
        Task<PurchaseGetByIdDTO> GetPurchaseByIdAsync(int purchaseId);
        Task CreatePurchase(PurchasePostDTO purchaseDto, bool isAuthenticated, int? userId);
        Task DeletePurchase(int purchaseId);
    }
    public class PurchaseService : IPurchaseService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private AppDbContext _appDbContext;

        public PurchaseService(UnitOfWork unitOfWork, IMapper mapper, AppDbContext appDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<PurchaseGetDTO>> GetPurchasesAsync()
        {
            return _mapper.Map<List<PurchaseGetDTO>>(await _unitOfWork.PurchaseRepository.GetAsync(includedProperties: ["Tickets", "User"]));
        }

        public async Task<List<PurchaseGetByIdDTO>> GetPurchasesByUserIdAsync(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            var purchases = _mapper.Map<List<PurchaseGetByIdDTO>>(await _appDbContext.Purchases.Include(p => p.Tickets).Include(p => p.User).Where(p => p.UserId == userId).ToListAsync());
            if (purchases == null) throw new KeyNotFoundException();

            return purchases;
        }

        
        public async Task<PurchaseGetByIdDTO> GetPurchaseByIdAsync(int purchaseId)
        {
            if (purchaseId <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseId));
            var purchase = _mapper.Map<PurchaseGetByIdDTO>(await _unitOfWork.PurchaseRepository.GetByIdAsync(purchaseId));
            if (purchase == null) throw new KeyNotFoundException();

            return purchase;
        }
        
        public async Task CreatePurchase(PurchasePostDTO purchaseDto, bool isAuthenticated, int? userId)
        {
            var purchase = _mapper.Map<Purchase>(purchaseDto);

            

            if (isAuthenticated)
            {
                if (userId == null) throw new Exception("User logged in but doesn't have userId");
                var user = await _unitOfWork.UserRepository.GetByIdAsync((int)userId, includedCollections: ["Roles"]);
                if (user == null) throw new KeyNotFoundException("User logged in but can not found by userId");

                if (user.Roles.Any(x => x.Name == "Cashier"))
                {
                    purchase.UserId = purchaseDto.UserId;
                }
                else if (user.Roles.Any(x => x.Name == "Customer"))
                {
                    purchase.UserId = user.Id;
                }
                else throw new AuthenticationException("Only Cashier and Customers can purchase");
                    purchase.Email = null;
                purchase.PhoneNumber = null;
            }
            else
            {
                if (purchaseDto.Email == null || purchaseDto.Email == string.Empty || purchaseDto.PhoneNumber ==null || purchaseDto.PhoneNumber == string.Empty)
                    throw new ArgumentException("Email and phone number are required");
                purchase.UserId = null;
            }


            purchase.Tickets.Clear();
            purchase.PurchaseDate = DateTime.UtcNow;
            var transaction = _appDbContext.Database.BeginTransaction();

            await _unitOfWork.PurchaseRepository.InsertAsync(purchase);
            await _unitOfWork.SaveAsync();

            foreach (var dto in purchaseDto.Tickets)
            {
                var screening = await _unitOfWork.ScreeningRepository.GetByIdAsync(dto.ScreeningId);
                if(screening == null)
                {
                    transaction.Rollback();
                    throw new KeyNotFoundException($"Screening with id({dto.ScreeningId}) not found. Purchase failed.");
                }
                var ticket = _mapper.Map<Ticket>(dto);
                ticket.PurchaseId = purchase.Id;
                ticket.Price = screening.DefaultTicketPrice;

                await _unitOfWork.TicketRepository.InsertAsync(ticket);
            }

            transaction.Commit();
        }

        public async Task DeletePurchase(int purchaseId)
        {
            if(purchaseId <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseId));
            var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(purchaseId);
            await _unitOfWork.PurchaseRepository.DeleteByIdAsync(purchaseId);
            await _unitOfWork.SaveAsync();
        }
    }
}
