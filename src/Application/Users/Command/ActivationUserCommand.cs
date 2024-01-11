using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Users.Command;

public record ActivationUserCommand(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string ProfileImg,
    Gender Gender,
    string Title,
    string[] Technologies)
    : IRequest
{
}

public class ActivationUserCommandHandler(IIdentityService identityService)
    : IRequestHandler<ActivationUserCommand>
{
    public async Task Handle(
        ActivationUserCommand request,
        CancellationToken cancellationToken)
    {
        await identityService.ActivateUserAsync(request);
    }
}
