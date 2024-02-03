namespace CoduTeam.Domain.Entities;

public class UserChat
{
    public int ChatId { get; set; }
    public int UserId { get; set; }
    public Chat? Chat { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}
