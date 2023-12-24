namespace CoduTeam.Application.Common.Validation;

public static class StringRules
{
    public static IRuleBuilderOptions<T, string> MustBeOnlyLetters<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        string fieldName)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"{fieldName} cannot be empty.")
            .NotNull().WithMessage($"{fieldName} cannot be null.")
            .Must(s => s.All(char.IsLetter)).WithMessage($"{fieldName} can only contain letters.");
    }
}
