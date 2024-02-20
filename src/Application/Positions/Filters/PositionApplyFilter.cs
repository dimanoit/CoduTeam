using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Filters;

public static class PositionApplyFilter
{
    public static IQueryable<PositionApply> AddPositionApplyStatusFilter(
        this IQueryable<PositionApply> dbQuery,
        PositionApplyStatus? status)
    {
        return status.HasValue
            ? dbQuery.Where(pa => pa.Status == status)
            : dbQuery;
    }
}
