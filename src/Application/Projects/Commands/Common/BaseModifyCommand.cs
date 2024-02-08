using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Commands.Common;

public abstract record BaseModifyCommand(
    string Title,
    string Description,
    ProjectCategory? Category,
    Country? Country,
    string? ProjectImgUrl)
{
}
