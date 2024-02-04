using Microsoft.AspNetCore.Identity;

namespace CoduTeam.Domain.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? Title { get; set; }
    public UserStatus UserStatus { get; set; }
    public ICollection<UserProject>? UserProjects { get; set; }
    public ICollection<UserChat>? UserChats { get; set; }
    public string? ImageSrc { get; set; }
    public string[] Technologies { get; set; } = Array.Empty<string>();
    public ICollection<PositionApply>? PositionApplies { get; set; }
}
