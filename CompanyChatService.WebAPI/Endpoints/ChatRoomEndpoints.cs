using CompanyChatService.Application.ChatRooms.Commands.CreateChatRoom;
using CompanyChatService.Application.ChatRooms.Queries.GetAllChatRooms;
using Mediator;

namespace CompanyChatService.WebAPI.Endpoints;

public class ChatRoomEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/chatrooms").WithTags("ChatRooms");

        group.MapPost("/", async (CreateChatRoomCommand command, IMediator mediator) =>
        {
            var chatRoomId = await mediator.Send(command);
            return Results.Created($"/api/chatrooms/{chatRoomId}", chatRoomId);
        })
        .WithName("CreateChatRoom");

        group.MapGet("/", async (IMediator mediator) =>
        {
            var query = new GetAllChatRoomsQuery();
            var chatRooms = await mediator.Send(query);
            return Results.Ok(chatRooms);
        })
        .WithName("GetAllChatRooms");
    }
}
