using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.PositionApplies;

public record PositionAppliesQuery : IRequest<PositionApplyResponse[]>;


public class PositionAppliesQueryHandler(IUser user, IApplicationDbContext dbContext) : IRequestHandler<PositionAppliesQuery, PositionApplyResponse[]>
{
    public async Task<PositionApplyResponse[]> Handle(PositionAppliesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.PositionApplies
            .Where(pa => pa.UserId == user.Id)
            .Select(pa => pa.ToPositionApplyResponse())
            .ToArrayAsync(cancellationToken) ?? Array.Empty<PositionApplyResponse>();
    }
}
