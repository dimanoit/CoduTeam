using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Commands.Common;

namespace CoduTeam.Application.Messages.Commands.DeleteMessageCommand;

public record DeleteMessageCommand(int messageId) : IRequest
{
}
public class DeleteMessageCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext) : IRequestHandler<DeleteMessageCommand>
{
    public async Task Handle(DeleteMessageCommand command, CancellationToken cancellationToken)
    {
        var message = await dbContext.Messages
            .Where(i => i.Id == command.messageId)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(command.messageId, message);

        identityService.ThrowIfNoAccessToResource(message);

        dbContext.Messages.Remove(message);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
