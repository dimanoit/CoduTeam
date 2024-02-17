namespace CoduTeam.Domain.Entities;

public class Message : BaseAuditableEntity
{
    public required int SenderId { get; set; }
    public required int ChatId { get; set; }
    public Chat? Chat { get; set; }
    public required string Content { get; set; }
}
