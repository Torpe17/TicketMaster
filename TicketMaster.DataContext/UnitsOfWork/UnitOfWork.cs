using TicketMaster.DataContext.Context;

namespace TicketMaster.DataContext.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    // Uncomment when implemented
    // IFilmRepository _filmRepository;
    // IPurchaseRepository _purchaseRepository;
    // IRoomRepository _roomRepository;
    // IScreeningRepository _screeningRepository;
    // ITicketRepository _ticketRepository;
    // IUserRepository _userRepository;
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}