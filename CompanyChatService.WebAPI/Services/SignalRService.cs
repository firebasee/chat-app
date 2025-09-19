using CompanyChatService.Application.Common.Interfaces;
using CompanyChatService.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CompanyChatService.WebAPI.Services;

public class SignalRService : ISignalRService
{
    private readonly IHubContext<ChatHub, IChatClient> _hubContext;

    public SignalRService(IHubContext<ChatHub, IChatClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageToChatRoom(Guid chatRoomId, string user, string message)
    {
        await _hubContext.Clients.Group(chatRoomId.ToString())
            .ReceiveMessage(user, message);
    }
}
