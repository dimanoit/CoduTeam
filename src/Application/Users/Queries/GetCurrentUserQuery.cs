using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Common.Security;
using CoduTeam.Application.Users.Models;

namespace CoduTeam.Application.Users.Queries;

[Authorize]
public record GetCurrentUserQuery(int userId) : IRequest<UserDto>;

public class GetCurrentUserQueryHandler(IIdentityService identityService)
    : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    public async Task<UserDto> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        return await identityService.GetUserDtoAsync(request.userId);
    }
}
