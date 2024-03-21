using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;
using CoduTeam.Domain.Events;

namespace CoduTeam.Application.PositionApplies;

public record ChangePositionApplyStatusCommand(int PositionApplyId, PositionApplyStatus Status) : IRequest;

public class ChangePositionApplyStatusCommandHandler(
    IApplicationDbContext dbContext,
    IPositionAppliesResourceValidator validator)
    : IRequestHandler<ChangePositionApplyStatusCommand>
{
    public async Task Handle(ChangePositionApplyStatusCommand request, CancellationToken cancellationToken)
    {
        PositionApply? application = await dbContext.PositionApplies
            .Include(pa => pa.Position)
            .ThenInclude(p => p!.Project)
            .FirstOrDefaultAsync(p => p.Id == request.PositionApplyId,
                cancellationToken);

        Guard.Against.NotFound(request.PositionApplyId, application);

        validator.ValidateStatusChange(application);

        application.Status = request.Status;
        application.AddDomainEvent(new PositionApplyStatusChangedEvent(application));

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
