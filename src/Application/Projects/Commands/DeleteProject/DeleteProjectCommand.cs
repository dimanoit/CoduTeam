using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(int Id) : IRequest;

public class DeleteProjectCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext)
    : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        Project? entity = await dbContext.Projects.Where(i => i.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        identityService.ThrowIfNoAccessToResource(entity);

        dbContext.Projects.Remove(entity);

        List<UserProject> entitiesToDelete = await dbContext.UserProjects
            .Where(i => i.ProjectId == entity.Id)
            .ToListAsync(cancellationToken);

        dbContext.UserProjects.RemoveRange(entitiesToDelete);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
