using CoduTeam.Api.Infrastructure;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Api.Endpoints;

public class Auth : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
