using CoduTeam.Application.Messages.Commands.CreateMessageCommand;
using CoduTeam.Application.Messages.Commands.UpdateMessageCommand;
using CoduTeam.Application.Messages.Models;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Messages.Mappers;

public static class MessageMapper
{
    public static Message ToMessage(this CreateMessageCommand command, int userId)
    {
        Message message = new() { SenderId = userId, ChatId = command.ChatId, Content = command.Content,SentTime = command.SentTime};
        return message;
    }
    public static MessageDto ToMessageDto(this Message message)
    {
        var messageDto = new MessageDto { SentTime = message.SentTime,Content = message.Content, SenderId = message.SenderId, Id = message.Id};
        return messageDto;
    }

    public static void MapUpdateMessage(this Message message, UpdateMessageCommand command)
    {
        message.Content = command.Content;
    }
}
