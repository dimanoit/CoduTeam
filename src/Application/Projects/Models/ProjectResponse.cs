using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Models;

public class ProjectResponse : ProjectDto
{
    public ICollection<ProjectParticipant> Participants { get; set; }
        = Array.Empty<ProjectParticipant>();
}

