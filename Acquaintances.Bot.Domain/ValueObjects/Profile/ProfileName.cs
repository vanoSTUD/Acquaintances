using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class ProfileName : ValueObject
{
    public const int NameLength = 20;

    public string Value { get; private set; }

    private ProfileName(string value)
    {
        Value = value;
    }

    public static Result<ProfileName> Create(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return Result.Failure<ProfileName>("Имя не может быть пустым");

        if (value.Length > NameLength)
            return Result.Failure<ProfileName>($"Имя не может быть больше {NameLength} символов");

        return new ProfileName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
