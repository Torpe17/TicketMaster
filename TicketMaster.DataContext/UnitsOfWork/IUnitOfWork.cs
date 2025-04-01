namespace TicketMaster.DataContext.UnitsOfWork;

public interface IUnitOfWork : IDisposable
{
    // IFilmRepository FilmRepository { get; }
    // IPurchaseRepository PurchaseRepository { get; }
    // IRoomRepository RoomRepository { get; }
    // IScreeningRepository ScreeningRepository { get; }
    // ITicketRepository TicketRepository { get; }
    // IUserRepository UserRepository { get; }
    void Save();
}