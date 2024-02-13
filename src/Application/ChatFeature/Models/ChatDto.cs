using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.ChatFeature.Models;

public class ChatDto
{
    public required int Id { get; set; }
    public required ChatType ChatType { get; set; }
    public required string Title { get; set; } 
    public required int[] Participants { get; set; }
}
