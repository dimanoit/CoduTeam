using CoduTeam.Application.PositionApplies;

namespace CoduTeam.Web.Endpoints;

public class PositionApplies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "position-applies")
            .RequireAuthorization()
            .MapPost(ApplyOnPosition)
            .MapPatch(AcceptApplicant, "acceptance")
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

    public async Task AcceptApplicant(ISender sender, AcceptApplicantCommand command)
    {
        await sender.Send(command);
    }
}
