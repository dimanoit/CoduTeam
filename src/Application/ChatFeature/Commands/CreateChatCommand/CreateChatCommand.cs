using CoduTeam.Application.Chat.Commands.Common;
using CoduTeam.Application.Chat.Mappers;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Chat.Commands.CreateChatCommand;

public record CreateChatCommand(
    ChatType ChatType) : BaseChatModifyCommand(ChatType), IRequest
{
}
public class CreateChatCommandHandler(IUser user, IApplicationDbContext dbContext) : IRequestHandler<CreateChatCommand>
{
    public async Task Handle(CreateChatCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        var chat = command.ToChat();
        dbContext.Chats.Add(chat);

        UserChat userChat = new() { UserId = user.Id.Value, Chat = chat };

        dbContext.UserChats.Add(userChat);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
