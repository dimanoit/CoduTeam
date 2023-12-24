using CoduTeam.Application.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace CoduTeam.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? Title { get; set; }
    public UserStatus UserStatus { get; set; }
}
