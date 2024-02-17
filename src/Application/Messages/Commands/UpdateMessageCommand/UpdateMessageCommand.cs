using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Commands.Common;
using CoduTeam.Application.Messages.Mappers;

namespace CoduTeam.Application.Messages.Commands.UpdateMessageCommand;

public record UpdateMessageCommand(int messageId, string Content, DateTimeOffset Created) : BaseModifyMessageCommand(Content, Created), IRequest;
public class UpdateMessageCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext) : IRequestHandler<UpdateMessageCommand>
{
    public async Task Handle(UpdateMessageCommand command, CancellationToken cancellationToken)
    {
        var message = await dbContext.Messages.FindAsync(command.messageId, cancellationToken);

        Guard.Against.NotFound(command.messageId, message);

        identityService.ThrowIfNoAccessToResource(message);

        message.MapUpdateMessage(command);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
