using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Filters;
using CoduTeam.Application.Projects.Mappers;
using CoduTeam.Application.Projects.Models;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Queries;

public record ProjectSearchQuery : IRequest<ProjectResponse[]?>
{
    public int? OwnerId { get; init; }
    public int? ProjectId { get; init; }
    public int? Take { get; init; }
    public int? Skip { get; init; }
    public ProjectCategory? Category { get; init; }
    public string? Term { get; init; }
    public bool? OnlyRelatedToCurrentUser { get; init; }
}

internal class SearchProjectsQueryHandler(IApplicationDbContext dbContext, IUser user)
    : IRequestHandler<ProjectSearchQuery, ProjectResponse[]?>
{
    public async Task<ProjectResponse[]?> Handle(ProjectSearchQuery query,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        ProjectResponse[] projectResponse = await dbContext
            .Projects
            .Include(p => p.UserProjects)
            .ThenInclude(ap => ap.ApplicationUser)
            .AddProjectIdFilter(query.ProjectId)
            .AddOnlyRelatedToCurrentUserFilter(query.OnlyRelatedToCurrentUser ?? false, user.Id.Value)
            .AddTermFilter(query.Term)
            .AddCategoryFilter(query.Category)
            .Skip(query.Skip ?? 0)
            .Take(query.Take ?? 5)
            .Select(project => project.ToProjectResponse())
            .ToArrayAsync(cancellationToken);

        return projectResponse;
    }
}
