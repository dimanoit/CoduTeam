using CoduTeam.Application.Projects.Models;
using CoduTeam.Application.Users.Models;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Users;

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

    public static ProjectParticipant ToParticipant(this ApplicationUser user)
    {
        Guard.Against.Null(user.UserName);
        return new ProjectParticipant(user.UserName, user.Id) { ImageSrc = user.ImageSrc };
    }
}
