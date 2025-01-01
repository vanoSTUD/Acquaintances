using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class ProfileDescription : ValueObject
{
	public const int DescriptionLength = 200;

	public string Value { get; private set; }

	private ProfileDescription(string value)
	{
		Value = value;
	}

	public static Result<ProfileDescription> Create(string value)
	{
		if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			value = "";

		if (value.Length > DescriptionLength)
			return Result.Failure<ProfileDescription>($"Описание не может быть больше {DescriptionLength} символов");

		return new ProfileDescription(value);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
