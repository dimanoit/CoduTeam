using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;
using FluentValidation.Results;
using ValidationException = CoduTeam.Application.Common.Exceptions.ValidationException;

namespace CoduTeam.Application.PositionApplies;

public record ApplyOnPositionCommand(int PositionId) : IRequest;

public class ApplyOnPositionCommandHandler(IUser user, IApplicationDbContext dbContext)
    : IRequestHandler<ApplyOnPositionCommand>
{
    public async Task Handle(ApplyOnPositionCommand request, CancellationToken cancellationToken)
    {
        Position? position =
            await dbContext.Positions.FirstOrDefaultAsync(p => p.Id == request.PositionId, cancellationToken);

        await ValidateRequestAsync(cancellationToken, position);

        PositionApply positionApply = new()
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
        List<ValidationFailure> validationErrors = new List<ValidationFailure>();

        if (position.PositionStatus == PositionStatus.Closed)
        {
            validationErrors.Add(new ValidationFailure(nameof(Position.PositionStatus), "Position already closed"));
        }

        bool isAlreadyApplied = await dbContext.PositionApplies
            .AnyAsync(pa => pa.PositionId == position.Id && pa.UserId == user.Id, cancellationToken);

        if (isAlreadyApplied)
        {
            validationErrors.Add(new ValidationFailure(nameof(ApplicationUser.Id), "User already applied on position"));
        }

        if (validationErrors.Count != 0)
        {
            throw new ValidationException(validationErrors);
        }
    }
}
