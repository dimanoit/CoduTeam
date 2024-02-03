using CoduTeam.Application.Common.Interfaces;

namespace CoduTeam.Application.Chat.Commands.Common;

public abstract class BaseModifyChatCommandValidator<T> : AbstractValidator<T> where T : BaseChatModifyCommand
{

    protected BaseModifyChatCommandValidator()
    {
        RuleFor(c => c.ChatType)
            .IsInEnum()
            .WithMessage("ChatType must be a valid value of the enum");
    }
}
