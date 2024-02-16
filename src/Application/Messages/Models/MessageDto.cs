using System.Runtime.InteropServices.JavaScript;

namespace CoduTeam.Application.Messages.Models;

public class MessageDto
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public int SenderId { get; set; }
    public DateTimeOffset Created { get; set; }
}
