using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Gender : ValueObject
{
	public const int MaxLength = 20;
	public static Gender Male => new (nameof(Male));
	public static Gender Female => new (nameof(Female));
	public static Gender All => new (nameof(All));
	
	private Gender(string value)
	{
		Value = value;
	}

	public string Value { get; private set; }

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
