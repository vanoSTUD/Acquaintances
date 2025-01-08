using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

/// <summary>
/// Лайк
/// </summary>
public class Like : Entity
{
	public const int MaxMessageLength = 100;
	public const int MinMessageLength = 1;

	private Like(long senderId, long recipientId, string? message = null)
	{
		SenderId = senderId;
		RecipientId = recipientId;
		Message = message;
	}

	// Для EF Core
	private Like() { }

	/// <summary>
	/// Id профиля - получателя лайка
	/// </summary>
	public long RecipientId { get; private set; }

	/// <summary>
	/// Id профиля - отправителя лайка
	/// </summary>
	public long SenderId { get; private set; }

	/// <summary>
	/// Сообщение от отправителя лайка
	/// </summary>
	public string? Message { get; private set; }


	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static Result<Like> Create(long senderId, long recipientId, string? message = null)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(senderId, nameof(senderId));
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(recipientId, nameof(recipientId));

		if (senderId == recipientId)
			throw new InvalidOperationException($"Недопустимо создание {nameof(Like)} с одинаковыми {nameof(RecipientId)} и {nameof(SenderId)} == '{senderId}'");

		if (message != null)
		{
			if (message.Length > MaxMessageLength)
				return Result.Failure<Like>($"Сообщение не может быть больше {MaxMessageLength} символов.");

			if (message.Length < MinMessageLength)
				return Result.Failure<Like>($"Сообщение не может быть меньше {MinMessageLength} символов.");
		}

		return new Like(senderId, recipientId, message);
	}
}
