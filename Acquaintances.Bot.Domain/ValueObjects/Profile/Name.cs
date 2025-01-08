using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Name : ValueObject
{
    public const int MaxLength = 25;
    public const int MinLength = 1;

    public string Value { get; private set; }

	[JsonConstructor]
	protected Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return Result.Failure<Name>("Имя не может быть пустым");

        if (value.Trim().Length > MaxLength)
            return Result.Failure<Name>($"Имя не может быть больше {MaxLength} символов(а)");

		if (value.Trim().Length < MinLength)
			return Result.Failure<Name>($"Имя не может быть меньше {MinLength} символов(а)");

		return new Name(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

	public override string ToString()
	{
        return Value;
	}
}
