namespace CoduTeam.Domain.Entities;

public class Message : BaseAuditableEntity
{
    public int SenderId { get; set; }
    public int ChatId { get; set; }
    public Chat? Chat { get; set; }
    public required string Content { get; set; } = "";
}
