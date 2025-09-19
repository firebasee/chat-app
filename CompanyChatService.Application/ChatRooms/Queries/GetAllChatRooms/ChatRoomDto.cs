namespace CompanyChatService.Application.ChatRooms.Queries.GetAllChatRooms;

public sealed record ChatRoomDto(
    Guid Id,
    string Name
);
