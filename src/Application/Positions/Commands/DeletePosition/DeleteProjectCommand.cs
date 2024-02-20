using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Positions.Commands.DeletePosition;

public record DeletePositionCommand(int Id) : IRequest;

public class DeletePositionCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext)
    : IRequestHandler<DeletePositionCommand>
{
    public async Task Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        Position? entity = await dbContext.Positions.Where(i => i.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        identityService.ThrowIfNoAccessToResource(entity);

        dbContext.Positions.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
