using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Age : ValueObject
{
	public const int MaxAge = 100;
	public const int MinAge = 18;

	[JsonConstructor]
	protected Age(int value)
	{
		Value = value;
	}

	public int Value { get; private set; }

	public static Result<Age> Create(string? value)
	{
		if (value == null || !int.TryParse(value, out int age))
		{
			return Result.Failure<Age>("Некорректный формат возраста.");
		}

		if (age > MaxAge)
			return Result.Failure<Age>($"Возраст не может быть больше {MaxAge} лет.");
		
		if (age < MinAge)
			return Result.Failure<Age>($"Возраст не может быть меньше {MinAge} лет.");

		return new Age(age);
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
