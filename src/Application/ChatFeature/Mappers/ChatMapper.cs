using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Json;
using CoduTeam.Application.Chat.Commands.CreateChatCommand;
using CoduTeam.Application.ChatFeature.Commands.UpdateChatCommand;
using CoduTeam.Application.ChatFeature.Models;

namespace CoduTeam.Application.Chat.Mappers;

public static class ChatMapper

{

    public static ChatResponse ToChatResponse(this Domain.Entities.Chat project)
    {
        var mapped = project.ToChatDto();

        return mapped;
    }
    public static Domain.Entities.Chat ToChat(this CreateChatCommand command)
    {
        Domain.Entities.Chat chat = new() { ChatType = command.ChatType };
        return chat;
    }
    public static ChatResponse ToChatDto(this Domain.Entities.Chat chat)
    {
        ChatResponse response = new() { Id = chat.Id, ChatType = chat.ChatType };
        return response;
    }

    public static void MapUpdateChat(this Domain.Entities.Chat chat, UpdateChatCommand command)
    {
        chat.ChatType = command.ChatType;
    }
}
