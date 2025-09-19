namespace CompanyChatService.Application.Common.Interfaces;

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
    Task UserJoined(string user);
    Task UserLeft(string user);
}
