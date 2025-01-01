using Acquaintances.Bot.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class Reciprocity : Entity
{
	private Reciprocity(long firstUserId, long secondUserId)
	{
		RecipientId = firstUserId;
		AdmirerId = secondUserId;
	}

	public long RecipientId { get; private set; }
	public long AdmirerId { get; private set; }
	public Profile Admirer { get; } = null!;

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static Reciprocity Create(long firstUserId, long secondUserId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(firstUserId);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(secondUserId);

		if (firstUserId == secondUserId)
			throw new InvalidOperationException($"Попытка создания взаимной симпатии для одинаковых id = '{firstUserId}'.");

		return new Reciprocity(firstUserId, secondUserId);
	}
}
