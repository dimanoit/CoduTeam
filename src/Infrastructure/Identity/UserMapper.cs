using CoduTeam.Application.Users.Models;

namespace CoduTeam.Infrastructure.Identity;

public static class UserMapper
{
    public static UserDto ToUserDto(this ApplicationUser account)
    {
        return new UserDto
        {
            Id = account.Id,
            Email = account.Email,
            FirstName = account.FirstName,
            LastName = account.LastName,
            DateOfBirth = account.DateOfBirth,
            Gender = account.Gender,
            Title = account.Title
        };
    }
}
