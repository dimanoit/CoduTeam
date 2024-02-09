using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Models;

namespace CoduTeam.Application.Messages.Queries;

public class MessageQuery : IRequest<MessageResponse>
{
    public int MessageId { get; set; }
}
internal sealed class GetMessageQueryHandler(IApplicationDbContext DbContext, IUser user) : IRequestHandler<MessageQuery, MessageResponse>
{
    public async Task<MessageResponse> Handle(MessageQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        var messageResponse = await DbContext.Messages
            .Where(message => message.Id == request.MessageId)
            .Select(message => new MessageResponse { Id = message.Id, Content = message.Content })
            .FirstOrDefaultAsync(cancellationToken);
        Guard.Against.Null(messageResponse, "Message with that id not found");
        return messageResponse;
    }
}
