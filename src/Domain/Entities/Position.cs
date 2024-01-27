namespace CoduTeam.Domain.Entities;

public class Position : BaseAuditableEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ShortDescription { get; set; }
    public required PositionStatus PositionStatus { get; set; }
    public DateTime Deadline { get; set; }

    public bool IsRemote { get; set; } = true;
    public required int ProjectId { get; init; }
    public Project Project { get; init; } = null!;
}
