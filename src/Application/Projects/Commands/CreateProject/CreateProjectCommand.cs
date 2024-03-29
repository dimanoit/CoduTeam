using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Commands.Common;
using CoduTeam.Application.Projects.Mappers;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(
    string Title,
    string Description,
    ProjectCategory? Category,
    Country? Country,
    string? ProjectImgUrl) : BaseModifyCommand(Title, Description, Category, Country, ProjectImgUrl), IRequest
{
}

public class CreateProjectCommandHandler(
    IUser user,
    IApplicationDbContext dbContext)
    : IRequestHandler<CreateProjectCommand>
{
    public async Task Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);

        Project project = command.ToProject();
        dbContext.Projects.Add(project);

        UserProject accountProject = new() { UserId = user.Id.Value, Project = project };

        dbContext.UserProjects.Add(accountProject);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
