using CoduTeam.Application.ChatFeature.Commands.CreateChatCommand;
using CoduTeam.Application.ChatFeature.Commands.DeleteChatCommand;
using CoduTeam.Application.ChatFeature.Commands.UpdateChatCommand;
using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Application.ChatFeature.Queries;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Models;
using CoduTeam.Application.Messages.Queries;
using CoduTeam.Infrastructure.Hubs;
using CoduTeam.Infrastructure.Hubs.ChatInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CoduTeam.Api.Endpoints;

public class Chats : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(BroadcastEndpoint, "broadcast")
            .MapPost(SendMessageToSpecificUser, "user")
            .MapPost(CreateChatEndpoint)
            .MapDelete(DeleteChatEndpoint, "{id}")
            .MapPut(UpdateChatEndpoint)
            .MapGet(GetChatEndpoint, "{Id}")
            .MapGet(GetAllChatEndpoint)
            .MapGet(GetMessagesFromChat, "{chatId}/messages")
            .MapPost(JoinChat, "{chatId}/join-chat/{userId}");
    }

    public async Task<MessageDto[]> GetMessagesFromChat(ISender sender, int chatId)
    {
        MessageDto[] messages = await sender.Send(new GetMessagesFromChatQuery(chatId));
        return messages;
    }

    public async Task BroadcastEndpoint(Test message, IHubContext<ChatHub> context)
    {
        await context.Clients.All.SendAsync(message.Message);
    }

    public async Task SendMessageToSpecificUser(Test2 message, IHubContext<ChatHub> context, IUser user)
    {
        // TODO
        // 0.1 Create Message Entity (Sender, Recipient, Id, Content(string))    
        // 1. Create interface of service IMessageService 
        // 3. Implement MessageService 
        // 2. Method Send Message -> Create Message object -> Save to DB -> Send via SignalR
        // 3. Method Get Chat Messages (1-1 Chat) -> Get all messages via db -> REST endpoint 

        await context.Clients.User(user?.Id.ToString() ?? "").SendAsync("KEK");
    }

    public async Task CreateChatEndpoint(ISender sender, CreateChatCommand command)
    {
        await sender.Send(command);
    }

    public async Task DeleteChatEndpoint(ISender sender, int id)
    {
        await sender.Send(new DeleteChatCommand(id));
    }

    public async Task UpdateChatEndpoint(ISender sender, UpdateChatCommand command)
    {
        await sender.Send(command);
    }

    public async Task<ChatDto?> GetChatEndpoint(ISender sender, int Id)
    {
        ChatQuery query = new ChatQuery(Id);
        ChatDto chats = await sender.Send(query);
        return chats;
    }

    public async Task<ICollection<ChatDto>?> GetAllChatEndpoint(ISender sender, [AsParameters] AllChatsQuery query)
    {
        return await sender.Send(query);
    }

    public async Task JoinChat(IHubContext<ChatHub> hubContext, int userId, int chatId)
    {
        await hubContext.Groups.AddToGroupAsync("chuj", chatId.ToString());
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
