using CompanyChatService.Application.Messages.Commands.SendMessage;
using CompanyChatService.Application.Messages.Queries.GetMessagesForRoom;
using Mediator;

namespace CompanyChatService.WebAPI.Endpoints;

public class MessageEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/messages").WithTags("Messages");

        group.MapPost("/", async (SendMessageCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        })
        .WithName("SendMessage");

        app.MapGet("/api/rooms/{chatRoomId}/messages", async (Guid chatRoomId, IMediator mediator) =>
        {
            var query = new GetMessagesForRoomQuery(chatRoomId);
            var messages = await mediator.Send(query);
            return Results.Ok(messages);
        })
        .WithName("GetMessagesForRoom");
    }
}
