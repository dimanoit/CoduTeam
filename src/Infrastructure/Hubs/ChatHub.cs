using System.Security.Claims;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Infrastructure.Hubs.ChatInterfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CoduTeam.Infrastructure.Hubs;

public sealed class ChatHub(IApplicationDbContext dbContext) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userIdString = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int userId;
        if (int.TryParse(userIdString, out userId))
        {
            var chatIds = await dbContext.UserChats
                .Include(userChat => userChat.Chat)
                .Where(userChat => userChat.UserId == userId) 
                .Select(userChat => userChat.ChatId)
                .ToListAsync();
                foreach (var chatId in chatIds)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
                    await Clients.Group(chatId.ToString()).SendAsync( $"{userId} has joined the group {chatId}.");

                }
        }

        
    }
    
}
