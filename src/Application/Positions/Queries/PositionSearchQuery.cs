using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.PositionApplies;
using CoduTeam.Application.Positions.Filters;
using CoduTeam.Application.Positions.Mappers;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Queries;

public record PositionSearchQuery : IRequest<PositionResponse[]>
{
    public int? PositionId { get; init; }
    public int? ProjectId { get; init; }
    public ProjectCategory? ProjectCategory { get; init; }
    public PositionCategory? PositionCategory { get; init; }
    public PositionApplyStatus? ApplicationStatus { get; init; }
    public int? Take { get; init; }
    public int? Skip { get; init; }
    public string? Term { get; init; }
    public bool? WithApplicationStatus { get; init; }
    public bool? WithApplicants { get; set; }
}

internal class SearchPositionsQueryHandler(IApplicationDbContext dbContext, IUser user, TimeProvider dateTime)
    : IRequestHandler<PositionSearchQuery, PositionResponse[]>
{
    public async Task<PositionResponse[]> Handle(PositionSearchQuery query,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        PositionResponse[] response = await dbContext
            .Positions.Include(p => p.Project)
            .Where(p => p.Status == PositionStatus.Opened)
            .Where(p => p.Deadline >= dateTime.GetUtcNow().Date)
            .AddProjectIdFilter(query.ProjectId)
            .AddPositionIdFilter(query.PositionId)
            .AddTermFilter(query.Term)
            .Select(position => position.ToPositionResponse())
            .Skip(query.Skip ?? 0)
            .Take(query.Take ?? 5)
            .ToArrayAsync(cancellationToken);

        if (query.WithApplicationStatus is true)
        {
            await AdjustWithStatuses(query.ApplicationStatus, response, cancellationToken);
        }

        if (query.WithApplicants is true)
        {
            await AdjustWithApplicants(response, cancellationToken);
        }

        return response;
    }

    private async Task AdjustWithApplicants(
        PositionResponse[] response,
        CancellationToken cancellationToken)
    {
        var positionsIds = response.Select(p => p.Id).ToArray();

        var applicants = await dbContext
            .PositionApplies
            .Include(pa => pa.User)
            .Where(pa => positionsIds.Contains(pa.PositionId))
            .Select(pa => pa.ToPositionApplicant())
            .ToArrayAsync(cancellationToken);

        var applicantsDictionary = applicants
            .GroupBy(pa => pa.PositionId)
            .ToDictionary(
                positionApplicants => positionApplicants.Key,
                group => group.ToArray());

        foreach (var positionResponseItem in response)
        {
            var isPositionHasApplicants = applicantsDictionary.ContainsKey(positionResponseItem.Id);
            if (isPositionHasApplicants)
            {
                positionResponseItem.Applicants = applicantsDictionary[positionResponseItem.Id];
            }
        }
    }

    private async Task AdjustWithStatuses(PositionApplyStatus? status,
        PositionResponse[] data,
        CancellationToken cancellationToken)
    {
        var applicationsDictionary = await dbContext.PositionApplies
            .Where(pa => pa.UserId == user.Id)
            .AddPositionApplyStatusFilter(status)
            .Select(pa => new { positionId = pa.PositionId, status = pa.Status })
            .ToDictionaryAsync(pa => pa.positionId, pa => pa.status, cancellationToken);

        foreach (var position in data)
        {
            var isUserAppliedOnPosition = applicationsDictionary.ContainsKey(position.Id);
            if (isUserAppliedOnPosition)
            {
                position.CurrentUserPositionApplyStatus = applicationsDictionary[position.Id];
            }
        }
    }
}
