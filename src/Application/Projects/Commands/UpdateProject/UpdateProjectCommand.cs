using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    int Id,
    string Title,
    string Description,
    Category? Category,
    Country? Country,
    string? ProjectImgUrl) : IRequest;

public class UpdateProjectCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        /* var entity = await dbContext.Projects
             .Include(p => p.UserProjects)
             .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);*/
        Project? entity = await dbContext.Projects.FindAsync(command.Id, cancellationToken);

        Guard.Against.NotFound(command.Id, entity);

        entity.Title = command.Title;
        entity.Description = command.Description;
        entity.Category = command.Category;
        entity.Country = command.Country;
        entity.ProjectImageUrl = command.ProjectImgUrl;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
