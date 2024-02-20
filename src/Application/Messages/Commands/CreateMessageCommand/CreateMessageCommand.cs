using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Commands.Common;
using CoduTeam.Application.Messages.Mappers;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Messages.Commands.CreateMessageCommand;

public record CreateMessageCommand(string Content, int ChatId, DateTimeOffset Created)
    : BaseModifyMessageCommand(Content, Created), IRequest
{
}

public class CreateMessageComandHandler(IUser user, IApplicationDbContext dbContext)
    : IRequestHandler<CreateMessageCommand>
{
    public async Task Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);
        Guard.Against.Default(user.Id.Value);

        Message message = command.ToMessage(user.Id.Value);
        // message.DomainEvents.A

        dbContext.Messages.Add(message);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
