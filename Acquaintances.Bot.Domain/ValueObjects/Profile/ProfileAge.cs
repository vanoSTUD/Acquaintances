using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class ProfileAge : ValueObject
{
	public const int MaxAge = 100;
	public const int MinAge = 18;

	public int Value { get; private set; }

	private ProfileAge(int value)
	{
		Value = value;
	}

	public static Result<ProfileAge> Create(int value)
	{
		if (value > MaxAge)
			return Result.Failure<ProfileAge>($"Возраст не может быть больше {MaxAge} лет.");
		
		if (value > MaxAge)
			return Result.Failure<ProfileAge>($"Возраст не может быть меньше {MinAge} лет.");

		return new ProfileAge(value);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
