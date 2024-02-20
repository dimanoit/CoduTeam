namespace CoduTeam.Infrastructure.Hubs.ChatInterfaces;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}
