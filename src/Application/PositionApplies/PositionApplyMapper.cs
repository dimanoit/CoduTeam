using CoduTeam.Application.Positions.Mappers;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.PositionApplies;

public static class PositionApplyMapper
{
    public static PositionApplyResponse ToPositionApplyResponse(this PositionApply entity)
    {
        return new PositionApplyResponse
        {
            Id = entity.Id, PositionId = entity.PositionId, UserId = entity.UserId, Status = entity.Status
        };
    }

    public static PositionResponse ToPositionResponse(this PositionApply entity)
    {
        var response = entity.Position!.ToPositionResponse();
        response.CurrentUserPositionApplyStatus = entity.Status;

        return response;
    }
}
