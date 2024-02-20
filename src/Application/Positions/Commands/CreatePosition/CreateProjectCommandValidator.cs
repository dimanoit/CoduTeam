using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Positions.Commands.Common;

namespace CoduTeam.Application.Positions.Commands.CreatePosition;

public class CreatePostionCommandValidator(IApplicationDbContext context)
    : BaseModifyPositionCommandValidator<CreatePositionCommand>(context);
