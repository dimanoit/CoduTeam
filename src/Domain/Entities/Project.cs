namespace CoduTeam.Domain.Entities;

public class Project : BaseAuditableEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Category? Category { get; set; }
    public Country? Country { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? ProjectImageUrl { get; set; }

    public ICollection<UserProject> UserProjects { get; set; } = null!;
}
