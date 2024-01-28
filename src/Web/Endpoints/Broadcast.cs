using CoduTeam.Infrastructure.Hubs;
using CoduTeam.Infrastructure.Hubs.ChatInterfaces;
using Microsoft.AspNetCore.SignalR;

namespace CoduTeam.Web.Endpoints;

public class Broadcast : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(BroadcastEndpoint);
    }

    public async Task BroadcastEndpoint(Test message, IHubContext<ChatHub, IChatClient> context)
    {
        await context.Clients.All.ReceiveMessage(message.Message);
    }
}

public class Test
{
    public required string Message { get; set; }
}
