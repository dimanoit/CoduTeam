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

public class ChatsEndpoint : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateChatEndpoint)
            .MapDelete(DeleteChatEndpoint, "{id}")
            .MapPut(UpdateChatEndpoint)
            .MapGet(GetChatEndpoint, "{Id}")
            .MapGet(GetAllChatEndpoint)
            .MapGet(GetMessagesFromChat, "{chatId}/messages");
    }

    public async Task<MessageDto[]> GetMessagesFromChat(ISender sender, int chatId)
    {
        MessageDto[] messages = await sender.Send(new GetMessagesFromChatQuery(chatId));
        return messages;
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
}
