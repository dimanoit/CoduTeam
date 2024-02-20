using CoduTeam.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;

namespace CoduTeam.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Test]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        IDictionary<string, string[]> actual = new ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        List<ValidationFailure> failures = new() { new ValidationFailure("Age", "must be over 18") };

        IDictionary<string, string[]> actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo("Age");
        actual["Age"].Should().BeEquivalentTo("must be over 18");
    }

    [Test]
    public void
        MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        List<ValidationFailure> failures = new()
        {
            new ValidationFailure("Age", "must be 18 or older"),
            new ValidationFailure("Age", "must be 25 or younger"),
            new ValidationFailure("Password", "must contain at least 8 characters"),
            new ValidationFailure("Password", "must contain a digit"),
            new ValidationFailure("Password", "must contain upper case letter"),
            new ValidationFailure("Password", "must contain lower case letter")
        };

        IDictionary<string, string[]> actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo("Password", "Age");

        actual["Age"].Should().BeEquivalentTo("must be 25 or younger", "must be 18 or older");

        actual["Password"].Should().BeEquivalentTo("must contain lower case letter", "must contain upper case letter",
            "must contain at least 8 characters", "must contain a digit");
    }
}
