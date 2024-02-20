using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.Projects.Commands.Common;

public abstract class BaseModifyProjectCommandValidator<T> : AbstractValidator<T> where T : BaseModifyCommand
{
    private readonly IApplicationDbContext _context;

    protected BaseModifyProjectCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(t => t.Title)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(BeUniqueTitle)
            .WithMessage("Title must be unique");

        RuleFor(d => d.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(c => c.Category)
            .IsInEnum()
            .WithMessage("Category must be a valid value of the enum");

        RuleFor(c => c.Country)
            .IsInEnum()
            .WithMessage("Country must be a valid value of the enum");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AllAsync(t => t.Title != title, cancellationToken);
    }
}
