using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

/// <summary>
/// Взаимная симпатия 
/// </summary>
public class Reciprocity : Entity
{
	private Reciprocity(long recipientId, long admirerId)
	{
		RecipientId = recipientId;
		AdmirerId = admirerId;
	}

	// Для EF Core
	private Reciprocity() { }

	/// <summary>
	/// Id получателя взаимной симпатии - AppUser
	/// </summary>
	public long RecipientId { get; private set; }

	/// <summary>
	/// Id анкеты, с которой есть взаимная симпатия
	/// </summary>
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
