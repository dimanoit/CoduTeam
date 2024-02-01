using CoduTeam.Application.Positions.Commands;

namespace CoduTeam.Web.Endpoints;

public class PositionApplies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "position-applies")
            .RequireAuthorization()
            .MapPost(ApplyOnPosition)
            .MapPatch(RejectApplicant, "rejection");
    }

    public async Task ApplyOnPosition(ISender sender, ApplyOnPositionCommand command)
    {
        await sender.Send(command);
    }

    public async Task RejectApplicant(ISender sender, RejectApplicantCommand command)
    {
        await sender.Send(command);
    }
}
