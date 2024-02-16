namespace CoduTeam.Application.Messages.Commands.Common;

public abstract record BaseModifyMessageCommand
{
    protected BaseModifyMessageCommand(string Content, DateTime SentTime)
    {
        this.Content = Content;
        this.SentTime = DateTime.UtcNow;
    }
    public string Content { get; init; }
    public DateTime SentTime { get; init; }
}

