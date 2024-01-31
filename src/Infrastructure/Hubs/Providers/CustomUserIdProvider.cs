using CoduTeam.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace CoduTeam.Infrastructure.Hubs.Providers;

public class CustomUserIdProvider(IHttpContextAccessor httpContextAccessor) : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        var userId = httpContextAccessor.HttpContext?.User?.Identity?.Name;
        return userId;
    }
}
