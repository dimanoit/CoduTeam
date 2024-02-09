using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Commands.Common;
using CoduTeam.Application.Messages.Mappers;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Messages.Commands.CreateMessageCommand;

public record CreateMessageCommand(string Content) : BaseModifyMessageCommand(Content), IRequest
{
}
public class CreateMessageComandHandler(IUser user, IApplicationDbContext dbContext) : IRequestHandler<CreateMessageCommand>
{
    public async Task Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        var message = command.ToMessage();

        dbContext.Messages.Add(message);

        var chat = await dbContext.Chats.FirstOrDefaultAsync(c => c.Id == message.ChatId, cancellationToken);

        if (chat != null)
        {
            chat.Messages.Add(message);
        }
        else
        {
            var newChat = new Domain.Entities.Chat();
            dbContext.Chats.Add(newChat);
            newChat.Messages = new List<Message> { message };
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
