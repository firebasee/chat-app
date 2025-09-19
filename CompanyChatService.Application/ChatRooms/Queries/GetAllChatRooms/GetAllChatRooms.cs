using CompanyChatService.Domain.Repositories;
using Mediator;

namespace CompanyChatService.Application.ChatRooms.Queries.GetAllChatRooms;

// 1. Query (The Request)
public sealed record GetAllChatRoomsQuery() : IQuery<IEnumerable<ChatRoomDto>>;


// 2. Query Handler (The Logic)
public sealed class GetAllChatRoomsQueryHandler : IQueryHandler<GetAllChatRoomsQuery, IEnumerable<ChatRoomDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllChatRoomsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<IEnumerable<ChatRoomDto>> Handle(GetAllChatRoomsQuery query, CancellationToken cancellationToken)
    {
        var chatRooms = await _unitOfWork.ChatRooms.GetAllAsync();

        return chatRooms.Select(cr => new ChatRoomDto(
            cr.Id,
            cr.Name
        ));
    }
}
