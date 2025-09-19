using CompanyChatService.Domain.Entities;

namespace CompanyChatService.Domain.Repositories;

public interface IMessageRepository : IGenericRepository<Message>
{
    Task<IEnumerable<Message>> GetMessagesWithSenderAsync(Guid chatRoomId);
}
