namespace CoduTeam.Domain.Entities;

public class Chat : BaseAuditableEntity
{
    public ChatType ChatType { get; set; }
    public string Title { get; set; } = "";
    public ICollection<UserChat> UserChats { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = null!;
}
