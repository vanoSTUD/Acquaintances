using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class City : ValueObject
{
	public const int MaxLength = 25;
	public const int MinLength = 1;

	private static readonly Regex _inputRegex = new("^[a-zA-Zа-яА-Я \\-`']+$");

	public string Value { get; private set; }

	[JsonConstructor]
	protected City(string value)
	{
		Value = value;
	}

	public static Result<City> Create(string? value)
	{
		if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			return Result.Failure<City>($"Название города не должно быть пустым.");

		if (_inputRegex.IsMatch(value) == false)
			return Result.Failure<City>($"Название города должно состоять только из русских и англ. букв.");

		if (value.Trim().Length < MinLength)
			return Result.Failure<City>($"Город не может быть меньше {MinLength} символов(а).");

		if (value.Trim().Length > MaxLength)
			return Result.Failure<City>($"Город не может быть длинее {MaxLength} символов(а).");

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
