using CompanyChatService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyChatService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Message> Messages { get; }
    DbSet<ChatRoom> ChatRooms { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
