using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Commands;

public record ApplyOnPositionCommand(int PositionId) : IRequest;

public class ApplyOnPositionCommandHandler(IUser user, IApplicationDbContext dbContext)
    : IRequestHandler<ApplyOnPositionCommand>
{
    public async Task Handle(ApplyOnPositionCommand request, CancellationToken cancellationToken)
    {
        Position? position =
            await dbContext.Positions.FirstOrDefaultAsync(p => p.Id == request.PositionId, cancellationToken);

        await ValidateRequestAsync(cancellationToken, position);

        PositionApply positionApply = new PositionApply
        {
            PositionId = request.PositionId,
            UserId = user.Id!.Value,
            Status = PositionApplyStatus.Sent
        };

        dbContext.PositionApplies.Add(positionApply);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateRequestAsync(CancellationToken cancellationToken, Position? position)
    {
        Guard.Against.Null(position);
        Guard.Against.Null(user.Id);

        if (position.PositionStatus == PositionStatus.Closed)
        {
            throw new ValidationException("Position already closed");
        }

        bool isAlreadyApplied = await dbContext.PositionApplies
            .AnyAsync(pa => pa.UserId == user.Id, cancellationToken);

        if (isAlreadyApplied)
        {
            throw new ValidationException("User already applied on this position");
        }
    }
}
