using CompanyChatService.Domain.Entities;
using CompanyChatService.Domain.Repositories;
using CompanyChatService.Infrastructure.Persistence;

namespace CompanyChatService.Infrastructure.Repositories;

public class ChatRoomRepository : GenericRepository<ChatRoom>, IChatRoomRepository
{
    public ChatRoomRepository(ApplicationDbContext context) : base(context)
    {
    }
}
