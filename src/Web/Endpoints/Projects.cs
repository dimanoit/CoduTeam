using CoduTeam.Application.Projects.Commands.CreateProject;
using CoduTeam.Application.Projects.Commands.DeleteProject;
using CoduTeam.Application.Projects.Commands.UpdateProject;
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
            .MapDelete(DeleteProject, "{Id}")
            .MapPut(UpdateProject)
            .MapGet(SearchProjects);
    }

    public async Task CreateProject(ISender sender, CreateProjectCommand command)
    {
        await sender.Send(command);
    }

    public async Task DeleteProject(ISender sender, int id)
    {
        await sender.Send(new DeleteProjectCommand(id));
    }

    public async Task UpdateProject(ISender sender, UpdateProjectCommand command)
    {
        await sender.Send(command);
    }

    public async Task<ProjectResponse[]?> SearchProjects(ISender sender, [AsParameters] ProjectSearchQuery query)
    {
        return await sender.Send(query);
    }
}
