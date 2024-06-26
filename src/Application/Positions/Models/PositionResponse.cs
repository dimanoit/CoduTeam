using CoduTeam.Application.Projects.Models;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Models;

public class PositionResponse
{
    public required int Id { get; init; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public required PositionCategory PositionCategory { get; set; }
    public PositionApplyStatus? CurrentUserPositionApplyStatus { get; set; }
    public ICollection<PositionApplicant>? Applicants { get; set; }
    public required ProjectDto Project { get; set; }
}
