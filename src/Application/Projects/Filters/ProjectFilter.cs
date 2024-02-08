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

    public static IQueryable<Project> AddProjectIdFilter(
        this IQueryable<Project> dbQuery,
        int? projectId)
    {
        return projectId.HasValue
            ? dbQuery.Where(p => p.Id == projectId.Value)
            : dbQuery;
    }



    public static IQueryable<Project> AddCategoryFilter(
        this IQueryable<Project> dbQuery,
        ProjectCategory? category)
    {
        return category.HasValue && category.Value != ProjectCategory.None
            ? dbQuery.Where(p => p.Category == category.Value.ToString())
            : dbQuery;
    }
}
