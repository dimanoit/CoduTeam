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
