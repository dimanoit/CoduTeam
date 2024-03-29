﻿using CoduTeam.Application.Messages.Commands.CreateMessageCommand;
using CoduTeam.Application.Messages.Commands.UpdateMessageCommand;
using CoduTeam.Application.Messages.Models;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Messages.Mappers;

public static class MessageMapper
{
    public static Message ToMessage(this CreateMessageCommand command, int userId)
    {
        Message message = new()
        {
            SenderId = userId,
            ChatId = command.ChatId,
            Content = command.Content,
            Created = command.Created
        };
        return message;
    }

    public static MessageDto ToMessageDto(this Message message)
    {
        MessageDto messageDto = new MessageDto
        {
            Content = message.Content,
            SenderId = message.SenderId,
            Id = message.Id,
            Created = message.Created
        };
        return messageDto;
    }

    public static void MapUpdateMessage(this Message message, UpdateMessageCommand command)
    {
        message.Content = command.Content;
    }
}
