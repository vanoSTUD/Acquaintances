using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class City : ValueObject
{
	public const int CityLength = 50;

	public string Value { get; private set; }

	[JsonConstructor]
	protected City(string value)
	{
		Value = value;
	}

	public static Result<City> Create(string? value)
	{
		if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			return Result.Failure<City>($"Не выявлено названия города.");

		if (value.Trim().Length > CityLength)
			return Result.Failure<City>($"Город не может быть длинее {CityLength} символов.");

		return new City(value);
	}

	public override string ToString()
	{
		return Value;
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
