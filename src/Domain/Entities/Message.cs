namespace CoduTeam.Domain.Entities;

public class Message : BaseAuditableEntity
{
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
    public int ChatId { get; set; }
    public Chat? Chat { get; set; }
    public ApplicationUser? Sender { get; set; }
    public ApplicationUser? Recipient { get; set; }
    public string? Content { get; set; }
}
