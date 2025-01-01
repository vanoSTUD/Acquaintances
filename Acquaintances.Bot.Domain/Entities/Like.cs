using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class Like : Entity
{
	private Like(long senderId, long recipientId)
	{
		SenderId = senderId;
		RecipientId = recipientId;
	}

	public long RecipientId { get; private set; }
	public long SenderId { get; private set; }
	public Profile Sender { get; private set; } = null!;

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static Like Create(long senderId, long recipientId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(senderId, nameof(senderId));
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(recipientId, nameof(recipientId));

		if (senderId == recipientId)
			throw new InvalidOperationException($"Недопустимо создание {nameof(Like)} с одинаковыми {nameof(RecipientId)} и {nameof(SenderId)} = '{senderId}'");

		return new Like(senderId, recipientId);
	}
}
