using CoduTeam.Application.Messages.Commands.CreateMessageCommand;
using CoduTeam.Application.Messages.Commands.UpdateMessageCommand;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Messages.Mappers;

public static class MessageMapper
{
    public static Message ToMessage(this CreateMessageCommand command)
    {
        Message message = new() { Content = command.Content };
        return message;
    }

    public static void MapUpdateMessage(this Message message, UpdateMessageCommand command)
    {
        message.Content = command.Content;
    }
}
