using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Application.Users.Command;
using CoduTeam.Application.Users.Models;
using CoduTeam.Application.Users.Queries;

namespace CoduTeam.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetCurrentUser)
            .MapPost(ActivateUser, "activation");
    }

    public async Task<UserDto> GetCurrentUser(ISender sender)
    {
        return await sender.Send(new GetCurrentUserQuery());
    }

    public async Task ActivateUser(
        ISender sender,
        IUser user,
        ActivationUserCommand command)
    {
        await sender.Send(command);
    }
}
