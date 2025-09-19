using CompanyChatService.Domain.Entities;
using CompanyChatService.Domain.Repositories;
using CompanyChatService.Infrastructure.Persistence;

namespace CompanyChatService.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
