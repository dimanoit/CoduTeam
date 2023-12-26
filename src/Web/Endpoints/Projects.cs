using CoduTeam.Application.Projects.Commands;
using CoduTeam.Application.Projects.Models;
using CoduTeam.Application.Projects.Queries;

namespace CoduTeam.Web.Endpoints;

public class Projects : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateProject)
            .MapGet(SearchProjects);
    }

    public async Task CreateProject(ISender sender, CreateProjectCommand command)
    {
        await sender.Send(command);
    }

    public async Task<ProjectResponse[]?> SearchProjects(ISender sender, [AsParameters] ProjectSearchQuery query)
    {
        return await sender.Send(query);
    }
}
