using CoduTeam.Application.Common.Validation;

namespace CoduTeam.Application.Users.Command;

public class ActivationUserCommandValidator : AbstractValidator<ActivationUserCommand>
{
    public ActivationUserCommandValidator()
    {
        RuleFor(v => v.DateOfBirth)
            .Must(ValidateAge)
            .WithMessage("The age must be between 10 and 100.");

        RuleFor(v => v.FirstName)
            .MustBeOnlyLetters("FirstName");

        RuleFor(v => v.LastName)
            .MustBeOnlyLetters("LastName");
    }

    private static bool ValidateAge(DateTime dateOfBirth)
    {
        int age = DateTime.Today.Year - dateOfBirth.Year;

        if (DateTime.Today < dateOfBirth.AddYears(age))
        {
            age--;
        }

        return age is >= 10 and <= 100;
    }
}
