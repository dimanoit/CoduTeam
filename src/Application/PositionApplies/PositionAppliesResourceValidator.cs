using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;
using FluentValidation.Results;

namespace CoduTeam.Application.PositionApplies;

public interface IPositionAppliesResourceValidator
{
    void ValidateStatusChange(PositionApply application);
}

public class PositionAppliesResourceValidator(IUser user) : IPositionAppliesResourceValidator
{
    public void ValidateStatusChange(PositionApply application)
    {
        var validationFailures = new List<ValidationFailure>();
        if (application.Position!.Project.CreatedBy != user.Id)
        {
            validationFailures.Add(new ValidationFailure(nameof(ApplicationUser.Id),
                "User don't have rights to edit this position apply"));
        }

        if (application.Position?.Status == PositionStatus.Closed)
        {
            validationFailures.Add(new ValidationFailure(nameof(Position.Status), "Position already closed"));
        }

        if (validationFailures.Count != 0)
        {
            throw new ValidationException(validationFailures);
        }
    }
}
