using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class Reciprocity : Entity
{
	private Reciprocity(long recipientId, long admirerId)
	{
		RecipientId = recipientId;
		AdmirerId = admirerId;
	}

	// Для EF Core
	private Reciprocity() { }

	public long RecipientId { get; private set; }
	public long AdmirerId { get; private set; }

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static Reciprocity Create(long recipientId, long admirerId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(recipientId);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(admirerId);

		if (recipientId == admirerId)
			throw new InvalidOperationException($"Попытка создания взаимной симпатии для одинаковых id = '{recipientId}'.");

		return new Reciprocity(recipientId, admirerId);
	}
}
