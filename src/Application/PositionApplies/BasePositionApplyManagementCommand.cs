using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.PositionApplies;

public abstract record BasePositionApplyManagementCommand(int PositionApplyId);

public abstract class BasePositionApplyManagementCommandValidator<T> : AbstractValidator<T>
    where T : BasePositionApplyManagementCommand
{
    protected BasePositionApplyManagementCommandValidator(IApplicationDbContext context, IUser user)
    {
        RuleFor(command => command.PositionApplyId)
            .MustAsync(async (positionApplyId, cancellationToken) =>
                await IsUserOwnerOfProjectAsync(context, user, positionApplyId, cancellationToken))
            .WithMessage("User doesn't have enough rules");
    }

    private async Task<bool> IsUserOwnerOfProjectAsync(
        IApplicationDbContext context,
        IUser user,
        int positionApplyId,
        CancellationToken cancellationToken)
    {
        bool isUserOwnerOfProject = await context.PositionApplies
            .Include(pa => pa.Position)
            .ThenInclude(p => p!.Project)
            .AnyAsync(pa => pa.Id == positionApplyId && pa.Position!.Project.CreatedBy == user.Id,
                cancellationToken);

        return isUserOwnerOfProject;
    }
}
