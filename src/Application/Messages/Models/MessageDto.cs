namespace CoduTeam.Application.Messages.Models;

public class MessageDto
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public int SenderId { get; set; }
    public DateTime SentTime { get; set; }
}
