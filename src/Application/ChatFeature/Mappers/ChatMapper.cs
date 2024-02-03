using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Json;
using CoduTeam.Application.Chat.Commands.CreateChatCommand;
using CoduTeam.Application.ChatFeature.Commands.UpdateChatCommand;

namespace CoduTeam.Application.Chat.Mappers;

public static class ChatMapper
{
    public static Domain.Entities.Chat ToChat(this CreateChatCommand command)
    {
        Domain.Entities.Chat chat = new() { ChatType = command.ChatType };
        return chat;
    }

    public static void MapUpdateChat(this Domain.Entities.Chat chat, UpdateChatCommand command)
    {
        chat.ChatType = command.ChatType;
    }
}
