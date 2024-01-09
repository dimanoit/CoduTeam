using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Commands.Common;
using CoduTeam.Application.Projects.Mappers;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    int Id,
    string Title,
    string Description,
    Category? Category,
    Country? Country,
    string? ProjectImgUrl) : BaseModifyCommand(Title, Description, Category, Country, ProjectImgUrl), IRequest;

public class UpdateProjectCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext)
    : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        Project? entity = await dbContext.Projects.FindAsync(command.Id, cancellationToken);

        Guard.Against.NotFound(command.Id, entity);
        identityService.ThrowIfNoAccessToResource(entity);

        entity.MapUpdateProject(command);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
