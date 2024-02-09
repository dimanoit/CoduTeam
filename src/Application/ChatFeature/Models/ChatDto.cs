using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.ChatFeature.Models;

public class ChatDto
{
    public int Id { get; set; }
    public ChatType ChatType { get; set; }
}
