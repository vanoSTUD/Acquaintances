using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Age : ValueObject
{
	public const int MaxAge = 100;
	public const int MinAge = 18;

	private Age(int value)
	{
		Value = value;
	}

	public int Value { get; private set; }

	public static Result<Age> Create(int value)
	{
		if (value > MaxAge)
			return Result.Failure<Age>($"Возраст не может быть больше {MaxAge} лет.");
		
		if (value < MinAge)
			return Result.Failure<Age>($"Возраст не может быть меньше {MinAge} лет.");

		return new Age(value);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
