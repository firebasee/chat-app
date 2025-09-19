using CompanyChatService.Domain.Entities;
using CompanyChatService.Domain.Repositories;
using CompanyChatService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyChatService.Infrastructure.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Message>> GetMessagesWithSenderAsync(Guid chatRoomId)
    {
        return await _context.Messages
            .Where(m => m.ChatRoomId == chatRoomId)
            .Include(m => m.Sender)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }
}