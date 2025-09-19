using CompanyChatService.Domain.Entities;

namespace CompanyChatService.Domain.Repositories;

public interface IMessageRepository : IGenericRepository<Message>
{
    // Mesajlara özel metotlar buraya eklenebilir
    // Örn: Task<IEnumerable<Message>> GetMessagesForRoomAsync(Guid roomId);
}
