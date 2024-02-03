using CoduTeam.Application.Chat.Commands.CreateChatCommand;
using CoduTeam.Application.ChatFeature.Commands.DeleteChatCommand;
using CoduTeam.Application.ChatFeature.Commands.UpdateChatCommand;
using CoduTeam.Application.Common.Interfaces;
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
            .MapPost(BroadcastEndpoint, "broadcast")
            .MapPost(SendMessageToSpecificUser, "user")
            .MapPost(CreateChatEndpoint)
            .MapDelete(DeleteChatEndpoint, "{id}")
            .MapPut(UpdateChatEndpoint);

    }

    public async Task BroadcastEndpoint(Test message, IHubContext<ChatHub, IChatClient> context)
    {
        await context.Clients.All.ReceiveMessage(message.Message);
    }

    public async Task SendMessageToSpecificUser(Test2 message, IHubContext<ChatHub, IChatClient> context, IUser user)
    {
        // TODO
        // 0.1 Create Message Entity (Sender, Recipient, Id, Content(string))    
        // 1. Create interface of service IMessageService 
        // 3. Implement MessageService 
        // 2. Method Send Message -> Create Message object -> Save to DB -> Send via SignalR
        // 3. Method Get Chat Messages (1-1 Chat) -> Get all messages via db -> REST endpoint 

        await context.Clients.User(user?.Id.ToString() ?? "").ReceiveMessage("KEK");
    }

    public async Task CreateChatEndpoint(ISender sender, CreateChatCommand command)
    {
        await sender.Send(command);
    }
    public async Task DeleteChatEndpoint(ISender sender, int id)
    {
        await sender.Send( new DeleteChatCommand(id));
    }
    public async Task UpdateChatEndpoint(ISender sender, UpdateChatCommand command)
    {
        await sender.Send(command);
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
