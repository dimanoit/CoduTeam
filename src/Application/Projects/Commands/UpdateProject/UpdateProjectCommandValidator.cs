using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Commands.Common;

namespace CoduTeam.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator
    : BaseModifyProjectCommandValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator(IApplicationDbContext context) : base(context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Please provide project id");
    }
}
