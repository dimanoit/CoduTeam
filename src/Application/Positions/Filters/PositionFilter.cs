using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Filters;

public static class PositionFilter
{
    public static IQueryable<Position> AddPositionIdFilter(
        this IQueryable<Position> dbQuery,
        int? positionId)
    {
        return positionId.HasValue
            ? dbQuery.Where(p => p.Id == positionId.Value)
            : dbQuery;
    }

    public static IQueryable<Position> AddProjectIdFilter(
        this IQueryable<Position> dbQuery,
        int? projectId)
    {
        return projectId.HasValue
            ? dbQuery.Where(position => position.ProjectId == projectId.Value)
            : dbQuery;
    }
    
    public static IQueryable<Position> AddProjectCategoryFilter(
        this IQueryable<Position> dbQuery,
       ProjectCategory? category)
    {
        return category.HasValue && category.Value != ProjectCategory.None
            ? dbQuery.Where(p => p.Project.Category == category.Value.ToString())
            : dbQuery;
    }
    
    public static IQueryable<Position> AddPositionCategoryFilter(
        this IQueryable<Position> dbQuery,
        PositionCategory? category)
    {
        return category.HasValue
            ? dbQuery.Where(p => p.Category == category.Value)
            : dbQuery;
    }

    public static IQueryable<Position> AddTermFilter(
        this IQueryable<Position> dbQuery,
        string? term)
    {
        return string.IsNullOrEmpty(term)
            ? dbQuery
            : dbQuery.Where(x =>
                x.Description.Contains(term) ||
                x.Title.Contains(term) ||
                x.Project.Title.Contains(term));
    }
}
