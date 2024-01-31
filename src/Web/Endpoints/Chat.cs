using CoduTeam.Infrastructure.Hubs;
using CoduTeam.Infrastructure.Hubs.ChatInterfaces;
using Microsoft.AspNetCore.SignalR;

namespace CoduTeam.Web.Endpoints;

public class Chat : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(BroadcastEndpoint,"broadcast")
            .MapPost(SendMessageToSpecificUser,"user");
    }

    public async Task BroadcastEndpoint(Test message, IHubContext<ChatHub, IChatClient> context)
    {
        await context.Clients.All.ReceiveMessage(message.Message);
    }

    public async Task SendMessageToSpecificUser(Test2 message, IHubContext<ChatHub, IChatClient> context)
    {
        await context.Clients.User(message.userId).ReceiveMessage(message.Message);
    }
}

public class Test
{
    public required string Message { get; set; }
}
public class Test2
{
    public required string Message { get; set; }
    public required string userId { get; set; } 
}
