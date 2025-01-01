using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class ProfileCity : ValueObject
{
	public const int CityLength = 50;

	public string Value { get; private set; }

	private ProfileCity(string value)
	{
		Value = value;
	}

	public static Result<ProfileCity> Create(string value)
	{
		if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			value = "";

		if (value.Length > CityLength)
			return Result.Failure<ProfileCity>($"Город не может быть длинее {CityLength} символов");

		return new ProfileCity(value);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
