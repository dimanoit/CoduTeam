using CoduTeam.Infrastructure.Hubs.ChatInterfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CoduTeam.Infrastructure.Hubs;

public sealed class ChatHub : Hub<IChatClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
    }
}
