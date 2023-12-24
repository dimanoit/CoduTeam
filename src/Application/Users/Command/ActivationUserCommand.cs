using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Users.Models;

namespace CoduTeam.Application.Users.Command;

public record ActivationUserCommand(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    Gender Gender,
    string Title)
    : IRequest
{
    public int UserId { get; set; }
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
