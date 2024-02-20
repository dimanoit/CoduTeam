using CoduTeam.Application.Chat.Mappers;
using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.ChatFeature.Queries;

public record ChatQuery(int ChatId) : IRequest<ChatDto>
{
}

internal sealed class GetChatQueryHandler(IApplicationDbContext dbContext, IUser user)
    : IRequestHandler<ChatQuery, ChatDto>
{
    public async Task<ChatDto> Handle(ChatQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);
        ChatDto? chatResponse = await dbContext.Chats
            .Include(chat => chat.UserChats)
            .Where(chat => chat.Id == request.ChatId)
            .Select(chat => chat.ToChatDto())
            .FirstOrDefaultAsync(cancellationToken);
        Guard.Against.Null(chatResponse, $"Chat with that ID{request.ChatId} not found");
        return chatResponse;
    }
}
