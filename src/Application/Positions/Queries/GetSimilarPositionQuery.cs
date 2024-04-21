using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Mappers;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Queries;

public record GetSimilarPositionQuery : IRequest<PositionResponse[]>
{
    public ProjectCategory ProjectCategory { get; init; }
    public PositionCategory PositionCategory { get; init; }
}

internal class GetSimilarPositionQueryHandler(
    IApplicationDbContext dbContext, IUser user, TimeProvider dateTime)
    : IRequestHandler<GetSimilarPositionQuery, PositionResponse[]>
{
    public async Task<PositionResponse[]> Handle(GetSimilarPositionQuery query,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        PositionResponse[] response = await dbContext
            .Positions.Include(p => p.Project)
            .Include(p => p.PositionApplies)
            .OrderBy(p => p.Created)
            .Where(p => p.Status == PositionStatus.Opened)
            .Where(p => p.Deadline == null || p.Deadline >= dateTime.GetUtcNow().Date)
            .Where(p => p.Project.Category == query.ProjectCategory.ToString() || p.Category == query.PositionCategory)
            .Where(p => p.PositionApplies!.All(pa => pa.UserId != user.Id))
            .Select(position => position.ToPositionResponse())
            .Take(10)
            .ToArrayAsync(cancellationToken);

        return response;
    }

}
