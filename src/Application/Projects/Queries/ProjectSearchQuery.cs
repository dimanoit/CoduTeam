using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Filters;
using CoduTeam.Application.Projects.Models;
using CoduTeam.Application.Users;

namespace CoduTeam.Application.Projects.Queries;

public record ProjectSearchQuery(
    int? OwnerId,
    int? ProjectId,
    int? Take,
    int? Skip,
    bool OnlyRelatedToCurrentUser = false)
    : IRequest<ProjectResponse[]?>
{
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
            .AddOnlyRelatedToCurrentUserFilter(query.OnlyRelatedToCurrentUser, user.Id.Value)
            .Select(project => new ProjectResponse
            {
                Id = project.Id,
                Title = project.Title,
                Category = project.Category,
                Description = project.Description,
                Country = project.Country,
                ProjectImgUrl = project.ProjectImageUrl,
                Participants = project.UserProjects
                    .Select(ap => ap.ApplicationUser!.ToParticipant())
                    .ToArray()
            })
            .ToArrayAsync(cancellationToken);

        return projectResponse;
    }
}
