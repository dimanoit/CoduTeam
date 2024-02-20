using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Interfaces;

public interface IMessageService
{
    Task SendMessageAsync(Message request);
}
