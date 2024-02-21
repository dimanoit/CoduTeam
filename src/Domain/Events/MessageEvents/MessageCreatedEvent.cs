namespace CoduTeam.Domain.Events.MessageEvents;

public class MessageCreatedEvent(Message message) : BaseEvent
{
    public Message Message => message;
}
