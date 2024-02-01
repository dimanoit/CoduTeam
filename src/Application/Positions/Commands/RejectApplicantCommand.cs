using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Commands;

public record RejectApplicantCommand(int PositionApplyId) : IRequest;

public class RejectApplicationCommandHandler(IUser user, IApplicationDbContext dbContext)
    : IRequestHandler<RejectApplicantCommand>
{
    public async Task Handle(RejectApplicantCommand request, CancellationToken cancellationToken)
    {
        bool isUserOwnerOfProject = await dbContext
            .PositionApplies
            .Include(pa => pa.Position)
            .ThenInclude(p => p!.Project)
            .AnyAsync(pa => pa.Id == request.PositionApplyId && pa.Position!.Project.CreatedBy == user.Id,
                cancellationToken);

        if (!isUserOwnerOfProject)
        {
            throw new ValidationException("User don't have enough rules");
        }

        PositionApply? application = await dbContext.PositionApplies
            .FirstOrDefaultAsync(p => p.Id == request.PositionApplyId,
                cancellationToken);

        Guard.Against.NotFound(request.PositionApplyId, application);

        application.Status = PositionApplyStatus.Rejected;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
