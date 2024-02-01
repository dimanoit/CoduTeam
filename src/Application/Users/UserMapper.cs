using CoduTeam.Application.Projects.Models;
using CoduTeam.Application.Users.Command;
using CoduTeam.Application.Users.Models;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

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
            Title = account.Title,
            ProfileImage = account.ImageSrc,
            Technologies = account.Technologies
        };
    }

    public static void MapUser(
        this ApplicationUser userToActivate,
        ActivationUserCommand userDto)
    {
        userToActivate.FirstName = userDto.FirstName;
        userToActivate.LastName = userDto.LastName;
        userToActivate.DateOfBirth = userDto.DateOfBirth;
        userToActivate.Gender = userDto.Gender;
        userToActivate.Title = userDto.Title;
        userToActivate.ImageSrc = userDto.ProfileImg;
        userToActivate.Technologies = userDto.Technologies ?? [];
        userToActivate.UserStatus = UserStatus.Active;
    }

    public static ProjectParticipant ToParticipant(this ApplicationUser user)
    {
        return new ProjectParticipant(userName: user.UserName!, userId: user.Id, firstName: user.FirstName!,
            lastName: user.LastName!, projectTitle: user.Title!)
        {
            ImageSrc = user.ImageSrc,
            Technologies = user.Technologies,
            Gender = user.Gender
        };
    }
}
