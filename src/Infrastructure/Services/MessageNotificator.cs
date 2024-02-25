using CoduTeam.Application.Common.Models;
using CoduTeam.Application.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Infrastructure.Hubs;
using CoduTeam.Infrastructure.Hubs.ChatInterfaces;
using Microsoft.AspNetCore.SignalR;

namespace CoduTeam.Infrastructure.Services;

public class MessageNotificator(IHubContext<ChatHub> hubContext) : IMessageNotificator
{
    public async Task SendMessageToChatAsync(int chatId, Message message)
    {
        try
        {
            await hubContext.Clients.Group(chatId.ToString()).SendAsync(message.Content);
        }
        catch (Exception ex)
        {
            throw new Exception("Error sending message", ex);
        }
    }
}
