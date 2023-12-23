using CoduTeam.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CoduTeam.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler : INotificationHandler<TodoItemCompletedEvent>
{
    private readonly ILogger<TodoItemCompletedEventHandler> _logger;

    public TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CoduTeam Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
