using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Commands.Common;
using CoduTeam.Application.Messages.EventHadlers;
using CoduTeam.Application.Messages.Mappers;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Events.MessageEvents;

namespace CoduTeam.Application.Messages.Commands.CreateMessageCommand;

public record CreateMessageCommand(string Content, int ChatId, DateTimeOffset Created)
    : BaseModifyMessageCommand(Content, Created), IRequest
{
}

public class CreateMessageComandHandler(IUser user, IApplicationDbContext dbContext,IMediator mediator)
    : IRequestHandler<CreateMessageCommand>
{
    public async Task Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);
        Guard.Against.Default(user.Id.Value);

        Message message = command.ToMessage(user.Id.Value);
        dbContext.Messages.Add(message);
        await mediator.Publish(new MessageCreatedEvent(message), cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
