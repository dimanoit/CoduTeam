using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.PositionApplies;

public static class PositionApplyMapper
{
    public static PositionApplyResponse ToPositionApplyResponse(this PositionApply entity)
    {
        return new PositionApplyResponse
        {
            Id = entity.Id,
            PositionId = entity.PositionId,
            UserId = entity.UserId,
            Status = entity.Status
        };
    }
}
