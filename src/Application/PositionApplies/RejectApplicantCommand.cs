using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.PositionApplies;

public record RejectApplicantCommand(int PositionApplyId)
    : BasePositionApplyManagementCommand(PositionApplyId), IRequest;

public class RejectApplicantCommandValidator(IApplicationDbContext context, IUser user)
    : BasePositionApplyManagementCommandValidator<RejectApplicantCommand>(context, user);

public class RejectApplicationCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<RejectApplicantCommand>
{
    public async Task Handle(RejectApplicantCommand request, CancellationToken cancellationToken)
    {
        PositionApply? application = await dbContext.PositionApplies
            .FirstOrDefaultAsync(p => p.Id == request.PositionApplyId,
                cancellationToken);

        Guard.Against.NotFound(request.PositionApplyId, application);

        application.Status = PositionApplyStatus.Rejected;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
