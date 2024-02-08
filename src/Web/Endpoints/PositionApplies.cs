using CoduTeam.Application.PositionApplies;

namespace CoduTeam.Web.Endpoints;

public class PositionApplies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "position-applies")
            .RequireAuthorization()
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

    public async Task<PositionApplyResponse[]> GetPositionApplies(ISender sender, PositionAppliesQuery query, CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    public async Task ChangePositionApplyStatus(
        ISender sender,
        ChangePositionApplyStatusCommand command,
        CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
