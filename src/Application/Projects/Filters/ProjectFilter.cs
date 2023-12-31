using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Filters;

public static class ProjectFilter
{
    public static IQueryable<Project> AddOnlyRelatedToCurrentUserFilter(
        this IQueryable<Project> dbQuery,
        bool onlyRelatedToCurrentUser,
        int currentUserId)
    {
        return onlyRelatedToCurrentUser
            ? dbQuery.Where(p => p.UserProjects.Select(u => u.UserId).Contains(currentUserId))
            : dbQuery;
    }

    public static IQueryable<Project> AddTermFilter(
        this IQueryable<Project> dbQuery,
        string? term)
    {
        return string.IsNullOrEmpty(term)
            ? dbQuery
            : dbQuery.Where(x => x.Description.Contains(term));
    }

    public static IQueryable<Project> AddCategoryFilter(
        this IQueryable<Project> dbQuery,
        Category? category)
    {
        return category.HasValue && category.Value != Category.None
            ? dbQuery.Where(p => p.Category == category.Value)
            : dbQuery;
    }
}
