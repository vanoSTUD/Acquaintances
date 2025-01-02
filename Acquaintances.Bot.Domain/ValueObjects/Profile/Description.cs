using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Description : ValueObject
{
	public const int DescriptionLength = 200;

	public string Value { get; private set; }

	private Description(string value)
	{
		Value = value;
	}

	public static Result<Description> Create(string value)
	{
		if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			value = "";

		if (value.Length > DescriptionLength)
			return Result.Failure<Description>($"Описание не может быть больше {DescriptionLength} символов");

		return new Description(value);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
