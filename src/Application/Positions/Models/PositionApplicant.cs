using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Positions.Models;

public record PositionApplicant
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }
    public string? ImageSrc { get; set; }
    public PositionApplyStatus Status { get; set; }
    public int PositionId { get; set; }
    public int PositionApplyId { get; set; }
}
 
