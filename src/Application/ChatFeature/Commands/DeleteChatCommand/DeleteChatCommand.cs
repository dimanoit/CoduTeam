using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.ChatFeature.Commands.DeleteChatCommand;

public record DeleteChatCommand(int Id) : IRequest;

public class DeleteChatCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext) : IRequestHandler<DeleteChatCommand>
{
    public async Task Handle(DeleteChatCommand command, CancellationToken cancellationToken)
    {
        var chat = await dbContext.Chats.Where(i => i.Id == command.Id)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(command.Id, chat);

        identityService.ThrowIfNoAccessToResource(chat);

        dbContext.Chats.Remove(chat);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
