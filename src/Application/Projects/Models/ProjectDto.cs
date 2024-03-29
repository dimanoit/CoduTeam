using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Projects.Models;

public class ProjectDto
{
    public int Id { get; init; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int OwnerId { get; set; }
    public ProjectCategory? Category { get; set; }
    public Country? Country { get; set; }
    public string? ProjectImgUrl { get; set; }
}
