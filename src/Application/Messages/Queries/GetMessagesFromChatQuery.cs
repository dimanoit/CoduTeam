using CoduTeam.Application.ChatFeature.Filters;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Mappers;
using CoduTeam.Application.Messages.Models;

namespace CoduTeam.Application.Messages.Queries;

public record GetMessagesFromChatQuery(int ChatId) : IRequest<MessageDto[]>;

public class GetMessagesFromChatQueryHandler(IApplicationDbContext dbContext): IRequestHandler<GetMessagesFromChatQuery,MessageDto[]>
{
    public async Task<MessageDto[]> Handle(GetMessagesFromChatQuery request, CancellationToken cancellationToken)
    {
        var messages = await dbContext.Chats
            .Include(ch => ch.Messages)
            .AddChatIdFilter(request.ChatId)
            .SelectMany(ch => ch.Messages.Select(m => m.ToMessageDto()))
            .ToArrayAsync(cancellationToken);
        return messages;
    }
}
    
