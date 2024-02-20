using CoduTeam.Api.Infrastructure;
using CoduTeam.Application.PositionApplies;
using CoduTeam.Application.Positions.Models;

namespace CoduTeam.Api.Endpoints;

public class PositionApplies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "position-applies")
            .RequireAuthorization()
            .MapGet(GetPositionApplies)
            .MapPost(ApplyOnPosition)
            .MapPatch(ChangePositionApplyStatus, "status");
    }

    public async Task ApplyOnPosition(
        ISender sender,
        ApplyOnPositionCommand command,
        CancellationToken cancellationToken)
    {
        await sender.Send(command);
    }

    public async Task<PositionResponse[]> GetPositionApplies(
        ISender sender,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new PositionAppliesQuery(), cancellationToken);
    }

    public async Task ChangePositionApplyStatus(
        ISender sender,
        ChangePositionApplyStatusCommand command,
        CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
