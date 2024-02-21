using CoduTeam.Application.Common.Models;
using CoduTeam.Application.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CoduTeam.Infrastructure.Services;

public class SignalRService(IHubContext<ChatHub> hubContext) : ISignalRService
{
    public async Task SendMessageToClientAsync(int userId, Message message)
    {
        try
        {
            await hubContext.Clients.User(userId.ToString()).SendAsync("Message Sent", message);
        }
        catch (Exception ex)
        {
            throw new Exception("Error sending message",ex);
        }
    }
}
