using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.ValueObjects.Like;

public class Message : ValueObject
{
	public const int MaxLength = 100;
	public const int MinLength = 1;

	public static Message Empty => new (string.Empty);

	public string Value { get; private set; }

	private Message(string value)
	{
		Value = value;
	}

	public static Result<Message> Create(string? value)
	{
		if (value == null)
			return Empty;

		if (value.Length > MaxLength)
			return Result.Failure<Message>($"Сообщение не может быть больше {MaxLength} символов.");
		
		if (value.Length < MinLength)
			return Result.Failure<Message>($"Сообщение не может быть меньше {MaxLength} символов.");

		return new Message(value);
	}

	public bool IsEmpty()
	{
		if (Value == null || string.IsNullOrEmpty(Value))
			return true;

		return false;
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
