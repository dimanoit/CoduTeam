using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.Positions.Commands.Common;

public abstract class BaseModifyPositionCommandValidator<T> : AbstractValidator<T> where T : BaseModifyPositionCommand
{
    private readonly IApplicationDbContext _context;

    protected BaseModifyPositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(t => t.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(d => d.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}
