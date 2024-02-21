using CoduTeam.Application.Messages.EventHadlers;
using CoduTeam.Application.Messages.Models;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Interfaces;

public interface IMessageNotificator
{
    Task SendMessageToClientAsync(int userId, Message message);
}
