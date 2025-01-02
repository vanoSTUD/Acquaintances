using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class City : ValueObject
{
	public const int CityLength = 50;

	public string Value { get; private set; }

	private City(string value)
	{
		Value = value;
	}

	public static Result<City> Create(string value)
	{
		if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			value = "";

		if (value.Length > CityLength)
			return Result.Failure<City>($"Город не может быть длинее {CityLength} символов");

		return new City(value);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
