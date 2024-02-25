using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Interfaces;
using CoduTeam.Application.Messages.Models;
using CoduTeam.Domain.Events.MessageEvents;

namespace CoduTeam.Application.Messages.EventHadlers;

public class MessageCreatedEventHandler(IUser user, IApplicationDbContext dbContext, IMessageNotificator messageNotificator) : INotificationHandler<MessageCreatedEvent>
{
    public async Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
    {
        var message = notification.Message;
        var chatId = message.ChatId;
        Guard.Against.Null(user.Id);

        await messageNotificator.SendMessageToChatAsync(chatId, message);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
