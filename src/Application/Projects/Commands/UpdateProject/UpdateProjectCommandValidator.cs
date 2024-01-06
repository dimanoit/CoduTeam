﻿using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Commands.UpdateProject;

namespace CoduTeam.Application.Projects.Commands;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(t => t.Title)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(BeUniqueTitle)
            .WithMessage("Title must be unique")
            .WithErrorCode("Unique");
        RuleFor(d => d.Description)
            .NotEmpty()
            .MaximumLength(500);
        RuleFor(c => c.Category)
            .IsInEnum()
            .WithMessage("Category must be a valid value of the enum");
        RuleFor(c => c.Country)
            .IsInEnum()
            .WithMessage("Country must be a valid value of the enum");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AllAsync(t => t.Title != title, cancellationToken);
    }
    
}