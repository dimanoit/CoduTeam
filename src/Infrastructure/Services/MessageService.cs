using CoduTeam.Application.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Infrastructure.Data;

namespace CoduTeam.Infrastructure.Services;

public class MessageService(ApplicationDbContext _dbContext) : IMessageService
{
    public async Task SendMessageAsync(Message request)
    {
        var message = new Message();
        message.Content = request.Content;
        message.Id = request.Id;
        message.Sender = request.Sender;
        message.Recipient = request.Recipient;

        _dbContext.Messages.Add(message);
        await _dbContext.SaveChangesAsync();
    }
}
