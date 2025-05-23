using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.Repositories;

namespace TicketMaster.DataContext.UnitsOfWork;

public class UnitOfWork
{
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        FilmRepository = new GenericRepository<Film>(context);
        PurchaseRepository = new GenericRepository<Purchase>(context);
        RoomRepository = new GenericRepository<Room>(context);
        ScreeningRepository = new GenericRepository<Screening>(context);
        TicketRepository = new GenericRepository<Ticket>(context);
        UserRepository = new GenericRepository<User>(context);
        AddressRepository = new GenericRepository<Address>(context);
        RoomTypeRepository = new GenericRepository<RoomType>(context);
    }
    private AppDbContext _context;
    public GenericRepository<Film> FilmRepository { get; set; }
    public GenericRepository<Purchase> PurchaseRepository { get; set; }
    public GenericRepository<Room> RoomRepository { get; set;  }
    public GenericRepository<Screening> ScreeningRepository { get; set; }
    public GenericRepository<Ticket> TicketRepository { get; set; }
    public GenericRepository<User> UserRepository { get; set; }
    public GenericRepository<Address> AddressRepository { get; set; }
    public GenericRepository<RoomType> RoomTypeRepository { get; set; }
    public void Dispose() { _context.Dispose(); }
    public void Save() { _context.SaveChanges(); }
    public async Task SaveAsync() { await _context.SaveChangesAsync(); }
}