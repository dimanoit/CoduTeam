using System.Security.Claims;
using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Api.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? Id
    {
        get
        {
            ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;

            Claim? nameIdentifierClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null && int.TryParse(nameIdentifierClaim.Value, out int id))
            {
                return id;
            }

            return null;
        }
    }
}
