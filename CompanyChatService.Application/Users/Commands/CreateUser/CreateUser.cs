using CompanyChatService.Domain.Entities;
using CompanyChatService.Domain.Repositories;
using Mediator;

namespace CompanyChatService.Application.Users.Commands.CreateUser;

// 1. Command (The Request)
public sealed record CreateUserCommand(
    string UserName
) : ICommand<Guid>;


// 2. Command Handler (The Logic)
public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // Kullanıcı adının benzersizliğini kontrol et (isteğe bağlı ama iyi bir pratik)
        var existingUser = await _unitOfWork.Users.FindAsync(u => u.UserName == command.UserName);
        if (existingUser.Any())
        {
            throw new ApplicationException($"User with username '{command.UserName}' already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = command.UserName
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return user.Id;
    }
}
