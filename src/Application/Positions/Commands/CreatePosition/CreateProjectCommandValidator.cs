using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Commands.Common;
using CoduTeam.Application.Projects.Commands.Common;
using CoduTeam.Application.Projects.Commands.CreateProject;

namespace CoduTeam.Application.Positions.Commands.CreatePosition;

public class CreatePostionCommandValidator(IApplicationDbContext context)
    : BaseModifyPositionCommandValidator<CreatePositionCommand>(context);
