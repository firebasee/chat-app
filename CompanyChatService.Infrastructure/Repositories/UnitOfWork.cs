using CompanyChatService.Domain.Repositories;
using CompanyChatService.Infrastructure.Persistence;

namespace CompanyChatService.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IMessageRepository Messages { get; private set; }
    public IUserRepository Users { get; private set; }
    public IChatRoomRepository ChatRooms { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Messages = new MessageRepository(_context);
        Users = new UserRepository(_context);
        ChatRooms = new ChatRoomRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
