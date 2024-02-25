using CoduTeam.Api.Infrastructure;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Api.Endpoints;

public class AuthEndpoint : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
