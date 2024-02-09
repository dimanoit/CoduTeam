namespace CoduTeam.Application.Messages.Commands.Common;

public abstract class BaseMessageModifyCommandValidator<T> : AbstractValidator<T> where T : BaseModifyMessageCommand
{
    protected BaseMessageModifyCommandValidator()
    {
        RuleFor(c => c.Content)
            .MaximumLength(100)
            .WithMessage("Message should be less then 100 symbols");
    }
}
