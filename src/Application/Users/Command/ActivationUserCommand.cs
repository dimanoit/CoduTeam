using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Users.Command;

public record ActivationUserCommand : IRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string? ProfileImg { get; set; }
    public Gender Gender { get; set; }
    public string? Title { get; set; }
    public string[] Technologies { get; set; } = Array.Empty<string>();
}

public class ActivationUserCommandHandler(
    IIdentityService identityService)
    : IRequestHandler<ActivationUserCommand>
{
    public async Task Handle(
        ActivationUserCommand request,
        CancellationToken cancellationToken)
    {
        SetUserPhotoIfNull(request);
        await identityService.ActivateUserAsync(request);
    }

    private void SetUserPhotoIfNull(ActivationUserCommand request)
    {
        if (!string.IsNullOrEmpty(request.ProfileImg))
        {
            return;
        }

        var profileImg = "https://randomuser.me/api/portraits";
        profileImg += request.Gender == Gender.Male ? "/men" : "/women";
        profileImg += "/" + Random.Shared.Next(1, 99) + ".jpg";

        request.ProfileImg = profileImg;
    }
}
