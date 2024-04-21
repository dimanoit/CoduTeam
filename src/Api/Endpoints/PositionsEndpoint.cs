using CoduTeam.Api.Infrastructure;
using CoduTeam.Application.Positions.Commands.CreatePosition;
using CoduTeam.Application.Positions.Commands.DeletePosition;
using CoduTeam.Application.Positions.Commands.UpdatePosition;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Application.Positions.Queries;

namespace CoduTeam.Api.Endpoints;

public class PositionsEndpoint : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreatePosition)
            .MapDelete(DeletePosition, "{id}")
            .MapPut(UpdatePosition)
            .MapGet(SearchPositions)
            .MapGet(GetSimilarPositions, "similar")
            .MapGet(GetPosition, "{id}");
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
    
    public async Task<PositionResponse?> GetPosition(ISender sender, int id)
    {
        PositionSearchQuery query = new() { PositionId = id, WithApplicants = true};
        PositionResponse[] positions = await sender.Send(query);

        return positions?.FirstOrDefault();
    }

    public async Task<PositionResponse[]> GetSimilarPositions(ISender sender, [AsParameters] GetSimilarPositionQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<PositionResponse[]?> SearchPositions(ISender sender, [AsParameters] PositionSearchQuery query)
    {
        return await sender.Send(query);
    }
}
