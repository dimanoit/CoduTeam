namespace CoduTeam.Application.Positions.Commands.Common;

public abstract record BaseModifyPositionCommand(
    string Title,
    string Description,
    bool? IsRemote)
{
}
