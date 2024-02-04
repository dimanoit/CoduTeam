using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Filters;
using CoduTeam.Application.Positions.Mappers;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Queries;

public record PositionSearchQuery : IRequest<PositionResponse[]?>
{
    public int? PositionId { get; init; }
    public int? ProjectId { get; init; }
    public int? Take { get; init; }
    public int? Skip { get; init; }
    public string? Term { get; init; }
}

internal class SearchPositionsQueryHandler(IApplicationDbContext dbContext, IUser user, TimeProvider dateTime)
    : IRequestHandler<PositionSearchQuery, PositionResponse[]?>
{
    public async Task<PositionResponse[]?> Handle(PositionSearchQuery query,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        var response = await dbContext
            .Positions
            .Include(p => p.Project)
            .Where(p => p.PositionStatus == PositionStatus.Opened)
            .Where(p => p.Deadline >= dateTime.GetUtcNow().Date)
            .AddProjectIdFilter(query.ProjectId)
            .AddPositionIdFilter(query.PositionId)
            .AddTermFilter(query.Term)
            .Select(position => position.ToPositionResponse())
            .Skip(query.Skip ?? 0)
            .Take(query.Take ?? 5)
            .ToArrayAsync(cancellationToken);

        return response;
    }
}
