using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.Chat.Commands.Common;

public abstract record BaseChatModifyCommand(ChatType ChatType,string Title)
{
}
