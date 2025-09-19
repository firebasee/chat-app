using CompanyChatService.Application.Users.Commands.CreateUser;
using Mediator;

namespace CompanyChatService.WebAPI.Endpoints;

public class UserEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").WithTags("Users");

        group.MapPost("/", async (CreateUserCommand command, IMediator mediator) =>
        {
            var userId = await mediator.Send(command);
            return Results.Created($"/api/users/{userId}", userId);
        })
        .WithName("CreateUser");
    }
}
