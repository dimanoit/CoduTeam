using CoduTeam.Application.Projects.Commands.UpdateProject;
using CoduTeam.Application.Projects.Models;
using CoduTeam.Application.Users;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Projects.Mappers;

public static class ProjectMapper
{
    public static ProjectResponse ToProjectResponse(this Project project)
    {
        var response = new ProjectResponse
        {
            Id = project.Id,
            Title = project.Title,
            Category = project.Category,
            Description = project.Description,
            Country = project.Country,
            ProjectImgUrl = project.ProjectImageUrl,
            Participants = project.UserProjects
                .Select(ap => ap.ApplicationUser!.ToParticipant())
                .ToArray()
        };

        return response;
    }

    public static void MapUpdateProject(this Project entity, UpdateProjectCommand command)
    {
        entity.Title = command.Title;
        entity.Description = command.Description;
        entity.Category = command.Category;
        entity.Country = command.Country;
        entity.ProjectImageUrl = command.ProjectImgUrl;
    }
}
