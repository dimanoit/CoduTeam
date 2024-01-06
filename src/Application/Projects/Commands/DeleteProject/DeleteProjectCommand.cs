using System.Data.Common;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Projects.Commands;

public record DeleteProjectCommand(int Id) : IRequest;

public class DeleteProjectCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Projects.
            Where(i => i.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        dbContext.Projects.Remove(entity);
        var entityToDelete = await dbContext.UserProjects
            .Where(i => i.ProjectId == entity.Id)
            .ToListAsync();
            
        dbContext.UserProjects.RemoveRange(entityToDelete);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
