using CoduTeam.Application.Positions.Commands;
using CoduTeam.Application.Positions.Commands.CreatePosition;
using CoduTeam.Application.Positions.Commands.DeletePosition;
using CoduTeam.Application.Positions.Commands.UpdatePosition;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Application.Positions.Queries;

namespace CoduTeam.Web.Endpoints;

public class Positions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreatePosition)
            .MapPost(ApplyOnPosition, "apply")
            .MapDelete(DeletePosition, "{id}")
            .MapPut(UpdatePosition)
            .MapGet(SearchPositions);
    }

    public async Task CreatePosition(ISender sender, CreatePositionCommand command)
    {
        await sender.Send(command);
    }

    public async Task DeletePosition(ISender sender, int id)
    {
        await sender.Send(new DeletePositionCommand(id));
    }

    public async Task UpdatePosition(ISender sender, UpdatePositionCommand command)
    {
        await sender.Send(command);
    }

    public async Task<PositionResponse[]?> SearchPositions(ISender sender, [AsParameters] PositionSearchQuery query)
    {
        return await sender.Send(query);
    }

    public async Task ApplyOnPosition(ISender sender, ApplyOnPositionCommand command)
    {
        await sender.Send(command);
    }
}
