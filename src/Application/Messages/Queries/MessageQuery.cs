using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Mappers;
using CoduTeam.Application.Messages.Models;

namespace CoduTeam.Application.Messages.Queries;

public class MessageQuery : IRequest<MessageDto>
{
    public int MessageId { get; set; }
}

internal sealed class GetMessageQueryHandler(IApplicationDbContext DbContext, IUser user)
    : IRequestHandler<MessageQuery, MessageDto>
{
    public async Task<MessageDto> Handle(MessageQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        MessageDto? messageResponse = await DbContext.Messages
            .Where(message => message.Id == request.MessageId)
            .Select(message => message.ToMessageDto())
            .FirstOrDefaultAsync(cancellationToken);
        Guard.Against.Null(messageResponse, $"Message with that id:{request.MessageId} not found");
        return messageResponse;
    }
}
