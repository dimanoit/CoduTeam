using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.ChatFeature.Commands.Common;

public abstract record BaseChatModifyCommand(ChatType ChatType, string Title)
{
}
