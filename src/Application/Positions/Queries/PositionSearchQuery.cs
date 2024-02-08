using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Filters;
using CoduTeam.Application.Positions.Mappers;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Queries;

public record PositionSearchQuery(
    int? PositionId,
    int? ProjectId,
    int? Take,
    int? Skip,
    string? Term,
    bool? WithApplicationStatus) : IRequest<PositionResponse[]?>;

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
            .Where(p => p.Status == PositionStatus.Opened)
            .Where(p => p.Deadline >= dateTime.GetUtcNow().Date)
            .AddProjectIdFilter(query.ProjectId)
            .AddPositionIdFilter(query.PositionId)
            .AddTermFilter(query.Term)
            .Select(position => position.ToPositionResponse())
            .Skip(query.Skip ?? 0)
            .Take(query.Take ?? 5)
            .ToArrayAsync(cancellationToken);

        if (query.WithApplicationStatus is null or false)
        {
            return response;
        }

        return await AdjustWithStatuses(response, cancellationToken);
    }

    private async Task<PositionResponse[]> AdjustWithStatuses(
        PositionResponse[] data,
        CancellationToken cancellationToken)
    {
        var applicationsDictionary = await dbContext.PositionApplies
            .Where(pa => pa.UserId == user.Id)
            .ToDictionaryAsync(pa => pa.PositionId, pa => pa.Status, cancellationToken);

        foreach (var position in data)
        {
            var isUserAppliedOnPosition = applicationsDictionary.ContainsKey(position.Id);
            if (isUserAppliedOnPosition)
            {
                position.CurrentUserPositionApplyStatus = applicationsDictionary[position.Id];
            }
        }

        return data;
    }
}
