namespace CompanyChatService.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IMessageRepository Messages { get; }
    IUserRepository Users { get; }
    IChatRoomRepository ChatRooms { get; }

    Task<int> CompleteAsync();
}
