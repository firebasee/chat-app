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

    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).UserJoinedRoom(Context.User?.Identity?.Name ?? "Anonymous", roomName);
    }

    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).UserLeftRoom(Context.User?.Identity?.Name ?? "Anonymous", roomName);
    }

    public async Task SendMessageToRoom(string roomName, string message)
    {
        await Clients.Group(roomName).ReceiveRoomMessage(Context.User?.Identity?.Name ?? "Anonymous", roomName, message);
    }
}
