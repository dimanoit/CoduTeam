using CoduTeam.Domain.Entities;

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
}
