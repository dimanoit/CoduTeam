using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Models;

public class ProjectResponse
{
    public int Id { get; init; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Category? Category { get; set; }
    public Country? Country { get; set; }
    public string? ProjectImgUrl { get; set; }
    public ICollection<ProjectParticipant> Participants { get; set; } = Array.Empty<ProjectParticipant>();
}

public class ProjectParticipant(string userName, int userId)
{
    public string UserName { get; } = userName;
    public int UserId { get; } = userId;
    public string? ImageSrc { get; set; }
}
