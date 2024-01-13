using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Commands.Common;
using CoduTeam.Application.Projects.Commands.UpdateProject;

namespace CoduTeam.Application.Positions.Commands.UpdatePosition;

public class UpdatePositionCommandValidator
    : BaseModifyProjectCommandValidator<UpdateProjectCommand>
{
    public UpdatePositionCommandValidator(IApplicationDbContext context) : base(context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Please provide project id");
    }
}
