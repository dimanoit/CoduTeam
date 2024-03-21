namespace CoduTeam.Application.Positions.Commands.Common;

public abstract class BaseModifyPositionCommandValidator<T> : AbstractValidator<T> where T : BaseModifyPositionCommand
{

    protected BaseModifyPositionCommandValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(d => d.Description)
            .NotEmpty()
            .MaximumLength(30_000);
    }
}
