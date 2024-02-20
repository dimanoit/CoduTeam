using CoduTeam.Application.Chat.Mappers;
using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Common.Security;
using CoduTeam.Domain.Constants;

namespace CoduTeam.Application.ChatFeature.Queries;

[Authorize(Roles = Roles.Administrator)]
public record AllChatsQuery : IRequest<ICollection<ChatDto>>;

internal sealed class GetAllChatQueryHandler(IApplicationDbContext dbContext, IUser user)
    : IRequestHandler<AllChatsQuery, ICollection<ChatDto>>
{
    public async Task<ICollection<ChatDto>> Handle(AllChatsQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        ChatDto[] chatResponses = await dbContext.Chats
            .Include(chat => chat.UserChats)
            .Select(chat => chat.ToChatDto())
            .ToArrayAsync(cancellationToken);
        return chatResponses;
    }
}
