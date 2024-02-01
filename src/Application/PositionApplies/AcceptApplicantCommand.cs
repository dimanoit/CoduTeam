using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;
using FluentValidation.Results;
using ValidationException = CoduTeam.Application.Common.Exceptions.ValidationException;

namespace CoduTeam.Application.PositionApplies;

public record AcceptApplicantCommand(int PositionApplyId)
    : BasePositionApplyManagementCommand(PositionApplyId), IRequest;

public class AcceptApplicantCommandValidator(IApplicationDbContext context, IUser user)
    : BasePositionApplyManagementCommandValidator<AcceptApplicantCommand>(context, user);

public record AcceptApplicantCommandHandler(IUser user, IApplicationDbContext dbContext)
    : IRequestHandler<AcceptApplicantCommand>
{
    public async Task Handle(
        AcceptApplicantCommand request,
        CancellationToken cancellationToken)
    {
        PositionApply? positionApply = await dbContext.PositionApplies
            .Include(pa => pa.Position)
            .ThenInclude(p => p!.Project)
            .FirstOrDefaultAsync(p => p.Id == request.PositionApplyId, cancellationToken);

        ValidatePositionApply(request, positionApply);

        UserProject accountProject = new() { UserId = user.Id!.Value, Project = positionApply!.Position!.Project };
        dbContext.UserProjects.Add(accountProject);

        positionApply.Position.PositionStatus = PositionStatus.Closed;
        positionApply.Status = PositionApplyStatus.Confirmed;

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private void ValidatePositionApply(AcceptApplicantCommand request, PositionApply? positionApply)
    {
        Guard.Against.NotFound(request.PositionApplyId, positionApply);
        var validationFailures = new List<ValidationFailure>();
        
        if (positionApply.Status == PositionApplyStatus.Rejected)
        {
            validationFailures.Add(new ValidationFailure(
                positionApply.Status.ToString(),
                "Applicant already rejected"));
        }

        if (positionApply.Position?.PositionStatus == PositionStatus.Closed)
        {
            validationFailures.Add(new ValidationFailure(
                positionApply.Position?.PositionStatus.ToString(),
                "Position already closed"));
        }

        if (validationFailures.Count != 0)
        {
            throw new ValidationException(validationFailures);
        }
    }
}
