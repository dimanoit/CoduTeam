using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Commands.Common;
using CoduTeam.Application.Positions.Mappers;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Commands.UpdatePosition;

public record UpdatePositionCommand(
    int Id,
    string Title,
    string Description,
    string ShortDescription,
    DateTime Deadline,
    PositionStatus PositionStatus,
    bool? IsRemote) : BaseModifyPositionCommand(Title, Description, ShortDescription, IsRemote), IRequest
{
}

public class UpdatePositionCommandHandler(IIdentityService identityService, IApplicationDbContext dbContext)
    : IRequestHandler<UpdatePositionCommand>
{
    public async Task Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
    {
        Position? entity = await dbContext.Positions.FindAsync(command.Id, cancellationToken);

        Guard.Against.NotFound(command.Id, entity);
        identityService.ThrowIfNoAccessToResource(entity);

        entity.MapUpdatePosition(command);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
