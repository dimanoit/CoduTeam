using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Models;

public class ProjectParticipant(
    string userName,
    int userId,
    string firstName,
    string lastName,
    string projectTitle)
{
    public string UserName { get; } = userName;
    public int UserId { get; } = userId;
    public string FirstName { get; } = firstName;
    public string LastName { get; set; } = lastName;
    public string ProjectTitle { get; set; } = projectTitle;
    public string? ImageSrc { get; set; }
    public Gender? Gender { get; set; }
    public string[] Technologies { get; set; } = Array.Empty<string>();
}
