using CoduTeam.Application.Projects.Commands.CreateProject;
using CoduTeam.Application.Projects.Commands.UpdateProject;
using CoduTeam.Application.Projects.Models;
using CoduTeam.Application.Users;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Mappers;

public static class ProjectMapper
{
    public static ProjectResponse ToProjectResponse(this Project project)
    {
        ProjectResponse mapped = project.ToProjectDto();
        mapped.Participants = project.UserProjects
            .Select(ap => ap.ApplicationUser!.ToParticipant())
            .ToArray();

        return mapped;
    }

    public static ProjectResponse ToProjectDto(this Project project)
    {
        ProjectResponse response = new ProjectResponse
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Country = project.Country,
            ProjectImgUrl = project.ProjectImageUrl
        };

        if (project.Category != null)
        {
            response.Category = (Category)Enum.Parse(typeof(Category), project.Category);
        }

        return response;
    }


    public static Project ToProject(this CreateProjectCommand command)
    {
        Project project = new()
        {
            Title = command.Title,
            Description = command.Description,
            Country = command.Country,
            ProjectImageUrl = command.ProjectImgUrl
        };

        if (command.Category != null)
        {
            project.Category = command.Category.Value.ToString();
        }

        return project;
    }

    public static void MapUpdateProject(this Project entity, UpdateProjectCommand command)
    {
        entity.Title = command.Title;
        entity.Description = command.Description;
        entity.Country = command.Country;
        entity.ProjectImageUrl = command.ProjectImgUrl;

        if (command.Category != null)
        {
            entity.Category = command.Category.Value.ToString();
        }
    }
}
