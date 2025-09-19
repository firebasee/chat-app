using CompanyChatService.Domain.Entities;

namespace CompanyChatService.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    // Kullanıcılara özel metotlar buraya eklenebilir
}
