namespace CoduTeam.Domain.Entities;

public class Chat : BaseAuditableEntity
{
    public ChatType ChatType { get; set; }
    public int UserChatId { get; set; }
    public int MessageId { get; set; }
    public ICollection<UserChat>? UserChats { get; set; }
    public ICollection<Message>? Messages { get; set; }
}
