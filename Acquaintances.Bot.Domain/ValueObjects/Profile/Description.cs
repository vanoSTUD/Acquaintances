using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Description : ValueObject
{
	public const int MaxLength = 900;
	public const int MinLength = 0;

	public string Value { get; private set; }

	[JsonConstructor]
	protected Description(string value)
	{
		Value = value;
	}

	public static Result<Description> Create(string? value)
	{
		if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			value = "";

		if (value.Trim().Length > MaxLength)
			return Result.Failure<Description>($"Описание не может быть больше {MaxLength} символов.");

		return new Description(value);
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
