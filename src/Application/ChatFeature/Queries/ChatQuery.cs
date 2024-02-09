using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Common.Models;

namespace CoduTeam.Application.ChatFeature.Queries;

public record ChatQuery() : IRequest<ChatResponse>
{
    public int ChatId { get; set; }
}

internal sealed class GetChatQueryHandler(IApplicationDbContext dbContext, IUser user)
    : IRequestHandler<ChatQuery, ChatResponse>
{
    public async Task<ChatResponse> Handle(ChatQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);
        var chatResponse = await dbContext.Chats
            .Where(chat => chat.Id == request.ChatId)
            .Select(chat => new ChatResponse { Id = chat.Id, ChatType = chat.ChatType })
            .FirstOrDefaultAsync(cancellationToken);
        Guard.Against.Null(chatResponse, "Chat with that ID not found");
        return chatResponse;
    }
}

