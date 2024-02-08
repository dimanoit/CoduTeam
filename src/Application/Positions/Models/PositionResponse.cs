using CoduTeam.Application.Projects.Models;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Models;

public class PositionResponse
{
    public required int Id { get; init; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ShortDescription { get; set; }
    public PositionApplyStatus? CurrentUserPositionApplyStatus { get; set; }
    public required ProjectDto Project { get; set; }
}

