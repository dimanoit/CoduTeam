using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Users.Models;

public record UserDto
{
    public int Id { get; set; }

    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ProfileImage { get; set; }
    public Gender? Gender { get; set; }
    public string? Title { get; set; }
    public string? Cv { get; set; }
}
