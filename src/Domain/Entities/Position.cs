namespace CoduTeam.Domain.Entities;

public class Position : BaseAuditableEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ShortDescription { get; set; }
    public required PositionStatus Status { get; set; }
    public PositionCategory Category { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsRemote { get; set; } = true;
    public required int ProjectId { get; init; }
    public Project Project { get; init; } = null!;
    public ICollection<PositionApply>? PositionApplies { get; set; }
}
