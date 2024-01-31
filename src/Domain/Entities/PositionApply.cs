namespace CoduTeam.Domain.Entities;

public class PositionApply : BaseAuditableEntity
{
    public required int PositionId { get; set; }
    public required int UserId { get; set; }
    public PositionApplyStatus Status { get; set; }
    public ApplicationUser? User { get; set; }
    public Position? Position { get; set; }
}
