using CompanyChatService.Application.Common.Interfaces;
using CompanyChatService.Domain.Entities;
using CompanyChatService.Domain.Repositories;
using Mediator;

namespace CompanyChatService.Application.Messages.Commands.SendMessage;

public sealed record SendMessageCommand(
    Guid ChatRoomId,
    string Content,
    Guid SenderId 
) : ICommand<Unit>;


public sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISignalRService _signalRService;

    public SendMessageCommandHandler(IUnitOfWork unitOfWork, ISignalRService signalRService)
    {
        _unitOfWork = unitOfWork;
        _signalRService = signalRService;
    }

    public async ValueTask<Unit> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var sender = await _unitOfWork.Users.GetByIdAsync(command.SenderId) ?? throw new ApplicationException("Sender not found");

        var message = new Message
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            ChatRoomId = command.ChatRoomId,
            SenderId = command.SenderId,
            Timestamp = DateTime.UtcNow
        };

        await _unitOfWork.Messages.AddAsync(message);
        await _unitOfWork.CompleteAsync();

        await _signalRService.SendMessageToChatRoom(command.ChatRoomId, sender.UserName, command.Content);

        return Unit.Value;
    }
}