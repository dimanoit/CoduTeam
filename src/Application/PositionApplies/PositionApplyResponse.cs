using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.PositionApplies;

public class PositionApplyResponse
{
    public required int Id { get; set; }
    public required int PositionId { get; set; }
    public required int UserId { get; set; }
    public PositionApplyStatus Status { get; set; }
}
