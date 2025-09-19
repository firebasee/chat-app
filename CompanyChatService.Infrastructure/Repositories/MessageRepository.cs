using CompanyChatService.Domain.Entities;
using CompanyChatService.Domain.Repositories;
using CompanyChatService.Infrastructure.Persistence;

namespace CompanyChatService.Infrastructure.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(ApplicationDbContext context) : base(context)
    {
    }
}
