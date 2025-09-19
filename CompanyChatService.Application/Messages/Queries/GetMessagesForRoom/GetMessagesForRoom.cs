using CompanyChatService.Domain.Repositories;
using Mediator;

namespace CompanyChatService.Application.Messages.Queries.GetMessagesForRoom;

// 1. Query (The Request)
public sealed record GetMessagesForRoomQuery(
    Guid ChatRoomId
) : IQuery<IEnumerable<MessageDto>>;


// 2. Query Handler (The Logic)
public sealed class GetMessagesForRoomQueryHandler : IQueryHandler<GetMessagesForRoomQuery, IEnumerable<MessageDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessagesForRoomQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<IEnumerable<MessageDto>> Handle(GetMessagesForRoomQuery query, CancellationToken cancellationToken)
    {
        var messages = await _unitOfWork.Messages.GetMessagesWithSenderAsync(query.ChatRoomId);

        return messages.Select(m => new MessageDto(
            m.Id,
            m.Content,
            m.Timestamp,
            m.SenderId,
            m.Sender?.UserName ?? "Unknown", // Sender null olabilir
            m.ChatRoomId
        ));
    }
}
