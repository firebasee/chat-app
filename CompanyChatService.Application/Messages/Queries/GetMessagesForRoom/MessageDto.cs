namespace CompanyChatService.Application.Messages.Queries.GetMessagesForRoom;

public sealed record MessageDto(
    Guid Id,
    string Content,
    DateTime Timestamp,
    Guid SenderId,
    string SenderUserName,
    Guid ChatRoomId
);
