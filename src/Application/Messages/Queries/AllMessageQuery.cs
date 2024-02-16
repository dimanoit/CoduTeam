using CoduTeam.Application.ChatFeature.Models;
using CoduTeam.Application.ChatFeature.Queries;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Common.Security;
using CoduTeam.Application.Messages.Mappers;
using CoduTeam.Application.Messages.Models;
using CoduTeam.Domain.Constants;

namespace CoduTeam.Application.Messages.Queries;
//only for test purposes
[Authorize(Roles = Roles.Administrator)]
public class AllMessageQuery : IRequest<ICollection<MessageDto>>;
internal sealed class GetAllMessageQueryHandler(IApplicationDbContext dbContext, IUser user)
    : IRequestHandler<AllMessageQuery, ICollection<MessageDto>>
{
    public async Task<ICollection<MessageDto>> Handle(AllMessageQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        var messageResponses = await dbContext.Messages
            .Select(message => message.ToMessageDto())
            .ToListAsync(cancellationToken);
        return messageResponses;
    }
}
