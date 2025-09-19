namespace CompanyChatService.Application.Common.Interfaces;

public interface ISignalRService
{
    Task SendMessageToChatRoom(Guid chatRoomId, string user, string message);
}
