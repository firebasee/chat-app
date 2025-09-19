using CompanyChatService.Domain.Entities;
using CompanyChatService.Domain.Repositories;
using Mediator;

namespace CompanyChatService.Application.ChatRooms.Commands.CreateChatRoom;

// 1. Command (The Request)
public sealed record CreateChatRoomCommand(
    string Name
) : ICommand<Guid>;


// 2. Command Handler (The Logic)
public sealed class CreateChatRoomCommandHandler : ICommandHandler<CreateChatRoomCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<Guid> Handle(CreateChatRoomCommand command, CancellationToken cancellationToken)
    {
        // Oda adının benzersizliğini kontrol et (isteğe bağlı ama iyi bir pratik)
        var existingRoom = await _unitOfWork.ChatRooms.FindAsync(cr => cr.Name == command.Name);
        if (existingRoom.Any())
        {
            throw new ApplicationException($"Chat room with name '{command.Name}' already exists.");
        }

        var chatRoom = new ChatRoom
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        await _unitOfWork.ChatRooms.AddAsync(chatRoom);
        await _unitOfWork.CompleteAsync();

        return chatRoom.Id;
    }
}
