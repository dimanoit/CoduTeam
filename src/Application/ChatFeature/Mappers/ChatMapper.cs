using CoduTeam.Application.Chat.Commands.CreateChatCommand;
using CoduTeam.Application.ChatFeature.Commands.UpdateChatCommand;
using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Chat.Mappers;

public static class ChatMapper

{
    public static Domain.Entities.Chat ToChat(this CreateChatCommand command)
    {
        Domain.Entities.Chat chat = new() { ChatType = command.ChatType, Title = command.Title };
        return chat;
    }
    public static ChatDto ToChatDto(this Domain.Entities.Chat chat)
    {
        ChatDto response = new() { Id = chat.Id, ChatType = chat.ChatType, Title = chat.Title, Participants = chat.UserChats.Select(userChats => userChats.UserId).ToArray() };
        return response;
    }

    public static void MapUpdateChat(this Domain.Entities.Chat chat, UpdateChatCommand command)
    {
        chat.ChatType = command.ChatType;
        chat.Title = command.Title;
    }
}
