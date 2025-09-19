using CompanyChatService.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CompanyChatService.WebAPI.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(string message)
    {
        await Clients.All.ReceiveMessage(Context.User?.Identity?.Name ?? "Anonymous", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.UserJoined(Context.User?.Identity?.Name ?? "Anonymous");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.All.UserLeft(Context.User?.Identity?.Name ?? "Anonymous");
        await base.OnDisconnectedAsync(exception);
    }
}
