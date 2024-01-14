using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Projects.Commands.Common;

namespace CoduTeam.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator(IApplicationDbContext context)
    : BaseModifyProjectCommandValidator<CreateProjectCommand>(context);
