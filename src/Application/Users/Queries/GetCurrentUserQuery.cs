using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Common.Security;
using CoduTeam.Application.Users.Models;

namespace CoduTeam.Application.Users.Queries;

[Authorize]
public record GetCurrentUserQuery : IRequest<UserDto>;

public class GetCurrentUserQueryHandler(IUser user, IIdentityService identityService)
    : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    public async Task<UserDto> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);
        return await identityService.GetUserDtoAsync(user.Id.Value);
    }
}
