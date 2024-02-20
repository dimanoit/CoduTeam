using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Models;

namespace CoduTeam.Application.PositionApplies;

public record PositionAppliesQuery
    : IRequest<PositionResponse[]>;


public class PositionAppliesQueryHandler(IUser user, IApplicationDbContext dbContext) 
    : IRequestHandler<PositionAppliesQuery, PositionResponse[]>
{
    public async Task<PositionResponse[]> Handle(
        PositionAppliesQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.PositionApplies
            .Include(pa => pa.Position)
            .ThenInclude(p => p!.Project)
            .Where(pa => pa.UserId == user.Id)
            .Select(pa => pa.ToPositionResponse())
            .ToArrayAsync(cancellationToken) ?? Array.Empty<PositionResponse>();
    }
}
