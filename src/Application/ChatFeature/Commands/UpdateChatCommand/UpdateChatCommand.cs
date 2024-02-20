using CoduTeam.Application.Chat.Mappers;
using CoduTeam.Application.ChatFeature.Commands.Common;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.ChatFeature.Commands.UpdateChatCommand;

public record UpdateChatCommand(
    int Id,
    ChatType ChatType,
    string Title) : BaseChatModifyCommand(ChatType, Title), IRequest;

public class UpdateChatCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext)
    : IRequestHandler<UpdateChatCommand>
{
    public async Task Handle(UpdateChatCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.Chat? chat = await dbContext.Chats.FindAsync(command.Id, cancellationToken);

        Guard.Against.NotFound(command.Id, chat);
        identityService.ThrowIfNoAccessToResource(chat);

        chat.MapUpdateChat(command);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
