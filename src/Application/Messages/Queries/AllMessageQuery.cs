using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Application.ChatFeature.Queries;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Messages.Models;

namespace CoduTeam.Application.Messages.Queries;
public class AllMessageQuery : IRequest<IEnumerable<MessageResponse>>
{

}
internal sealed class GetAllMessageQueryHandler(IApplicationDbContext dbContext, IUser user)
    : IRequestHandler<AllMessageQuery, IEnumerable<MessageResponse>>
{
    public async Task<IEnumerable<MessageResponse>> Handle(AllMessageQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        var messageResponses = await dbContext.Messages
            .Select(message => new MessageResponse { Id = message.Id, Content = message.Content })
            .ToListAsync(cancellationToken);
        return messageResponses;
    }
}
