using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Services
{
    public interface IPurchaseService
    {
        Task<List<PurchaseGetDTO>> GetPurchasesAsync();
        Task<List<PurchaseGetDTO>> GetPurchasesByUserIdAsync(int userId);
        Task<PurchaseGetDTO> GetPurchaseByIdAsync(int purchaseId);
        Task CreatePurchase(PurchasePostDTO purchaseDto, bool isAuthenticated, int? userId);
        Task DeletePurchase(int purchaseId);
        Task<bool> CanUserDeletePurchaseAsync(int purchaseId, int userId, bool isAdminOrCashier);
    }
    public class PurchaseService : IPurchaseService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private AppDbContext _appDbContext;
        private ITicketService _ticketService;

        public PurchaseService(UnitOfWork unitOfWork, IMapper mapper, AppDbContext appDbContext, ITicketService ticketService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _ticketService = ticketService;
        }

        public async Task<List<PurchaseGetDTO>> GetPurchasesAsync()
        {
            return _mapper.Map<List<PurchaseGetDTO>>(await _appDbContext.Purchases
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Screening)
                        .ThenInclude(s => s.Film)
                .Include(p => p.User)
                .ToListAsync());
        }

        public async Task<List<PurchaseGetDTO>> GetPurchasesByUserIdAsync(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            var purchases = _mapper.Map<List<PurchaseGetDTO>>(await _appDbContext.Purchases
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Screening)
                        .ThenInclude(s => s.Film)
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .ToListAsync());
            if (purchases == null) throw new KeyNotFoundException();

            return purchases;
        }

        
        public async Task<PurchaseGetDTO> GetPurchaseByIdAsync(int purchaseId)
        {
            if (purchaseId <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseId));
            var purchase = _mapper.Map<PurchaseGetDTO>(await _appDbContext.Purchases
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Screening)
                        .ThenInclude(t => t.Film)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == purchaseId));
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

                if (user.Roles.Any(x => x.Name == "Cashier")) { purchase.UserId = purchaseDto.UserId; }
                else if (user.Roles.Any(x => x.Name == "Customer")) { purchase.UserId = user.Id; }
                else throw new AuthenticationException("Only Cashier and Customers can purchase");
                
                purchase.Email = null;
                purchase.PhoneNumber = null;
            }
            else
            {
                if (string.IsNullOrEmpty(purchaseDto.Email) || string.IsNullOrEmpty(purchaseDto.PhoneNumber)) throw new ArgumentException("Email and phone number are required");
                purchase.UserId = null;
            }
            purchase.PurchaseDate = DateTime.UtcNow;

            //var screening = await _unitOfWork.ScreeningRepository.GetByIdAsync(purchase.Tickets.First().ScreeningId);
            var screening = await _appDbContext.Screenings.Include(x => x.Room).Where(x => x.Id == purchase.Tickets.First().ScreeningId).FirstAsync();
            if (screening == null) { throw new KeyNotFoundException("Screening not found"); }
            if(screening.Room.MaxSeatColumn == null)
            {
                if(purchase.Tickets.Count() > (screening.Room.Capacity)) //todo
                foreach (var item in purchase.Tickets)
                {
                    item.SeatRow = null;
                    item.SeatColumn = null;
                }
            }
            else
            {
                foreach (var item in purchase.Tickets)
                {
                    if (item.SeatRow <= 0 || item.SeatRow > screening.Room.MaxSeatRow) throw new ArgumentException("Seat row invalid");
                    if (item.SeatColumn <= 0 || item.SeatColumn > screening.Room.MaxSeatColumn) throw new ArgumentException("Seat column invalid");
                }
            }

            purchase.Tickets.ForEach(t => t.Price = screening.DefaultTicketPrice);
            
            await _unitOfWork.PurchaseRepository.InsertAsync(purchase);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePurchase(int purchaseId)
        {
            if(purchaseId <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseId));
            var purchase = _appDbContext.Purchases
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Screening)
                .Include(p => p.User)
                .FirstOrDefault(p => p.Id == purchaseId);
            
            var firstTicket = purchase.Tickets.FirstOrDefault();
            if (firstTicket?.Screening != null) { }
            {
                TimeSpan diff = firstTicket.Screening.Date - DateTime.UtcNow;
                if (diff.TotalHours < 4) { throw new InvalidOperationException("You cannot delete tickets 4 hours before screening."); }
            }
            await _unitOfWork.PurchaseRepository.DeleteByIdAsync(purchaseId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> CanUserDeletePurchaseAsync(int purchaseId, int userId, bool isAdminOrCashier)
        {
            var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(purchaseId);
            if (purchase == null) throw new KeyNotFoundException($"Purchase with id({purchaseId}) not found.");
            if (isAdminOrCashier) { return true; }

            return purchase.UserId == userId;
        }
    }
}
