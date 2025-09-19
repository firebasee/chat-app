namespace CompanyChatService.Application.Common.Interfaces;

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
    Task UserJoined(string user);
    Task UserLeft(string user);
    Task UserJoinedRoom(string user, string roomName);
    Task UserLeftRoom(string user, string roomName);
    Task ReceiveRoomMessage(string user, string roomName, string message);
}
