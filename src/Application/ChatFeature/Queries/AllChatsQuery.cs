using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.ChatFeature.Queries;

public class AllChatsQuery : IRequest<IEnumerable<ChatResponse>>
{
    
}
internal sealed class GetAllChatQueryHandler(IApplicationDbContext dbContext, IUser user)
    : IRequestHandler<AllChatsQuery, IEnumerable<ChatResponse>>
{
    public async Task<IEnumerable<ChatResponse>> Handle(AllChatsQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);
        
        var chatResponses = await dbContext.Chats
            .Select(chat => new ChatResponse { Id = chat.Id, ChatType = chat.ChatType })
            .ToListAsync(cancellationToken);
        return chatResponses;
    }
}
