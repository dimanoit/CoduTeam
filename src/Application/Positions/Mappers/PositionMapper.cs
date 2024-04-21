using CoduTeam.Application.Positions.Commands.UpdatePosition;
using CoduTeam.Application.Positions.Models;
using CoduTeam.Application.Projects.Mappers;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Positions.Mappers;

public static class PositionMapper
{
    public static PositionResponse ToPositionResponse(this Position position)
    {
        return new PositionResponse()
        {
            Id = position.Id,
            Title = position.Title,
            PositionCategory = position.Category,
            Description = position.Description,
            CreationDate = position.Created.Date,
            Project = position.Project.ToProjectDto(),
        };
    }

    public static PositionApplicant ToPositionApplicant(this ApplicationUser user)
    {
        return new PositionApplicant()
        {
            FirstName = user.FirstName,
            LastName = user.FirstName,
            ImageSrc = user.ImageSrc,
            Title = user.Title
        };
    }
    
    public static void MapUpdatePosition(this Position entity, UpdatePositionCommand command)
    {
        entity.Title = command.Title;
        entity.Description = command.Description;
        entity.IsRemote = command.IsRemote ?? true;
        entity.Deadline = command.Deadline;
        entity.Status = command.PositionStatus;
    }
}
