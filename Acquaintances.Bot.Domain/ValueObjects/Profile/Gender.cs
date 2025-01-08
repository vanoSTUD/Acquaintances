using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Gender : ValueObject
{
	public const int MaxLength = 20;
	public static Gender Male => new ("Парень");
	public static Gender Female => new ("Девушка");
	public static Gender All => new ("Все равно");

	private static readonly List<Gender> _genderList = [Male, Female, All];

	[JsonConstructor]
	protected Gender(string value)
	{
		Value = value;
	}

	public string Value { get; private set; }

	public static Result<Gender> Create(string? value, bool isPreferred)
	{
		if (value == null || !_genderList.Any(x => x.Value == value))
			return Result.Failure<Gender>("Некорректный формат.");
		
		if (isPreferred == false && value == All.Value)
			return Result.Failure<Gender>("Некорректный формат.");
		
		return new Gender(value);
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
