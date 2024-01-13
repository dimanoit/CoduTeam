using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Commands.Common;

public abstract record BaseModifyPositionCommand(
    string Title,
    string Description,
    string ShortDescription,
    bool? IsRemote)
{
}
