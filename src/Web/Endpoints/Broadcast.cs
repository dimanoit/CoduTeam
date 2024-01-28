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

    public async Task BroadcastEndpoint(string message, IHubContext<ChatHub, IChatClient> context)
    {
        await context.Clients.All.ReceiveMessage(message);
    }
}
