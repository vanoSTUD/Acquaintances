using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class Photo : Entity
{ 
	private Photo(long sourceId, long ownerId)
	{
		SourceId = sourceId;
		OwnerId = ownerId;
	}

	// Для EF Core
	private Photo() { }

	public long OwnerId { get; private set; }
	public long SourceId { get; private set; }

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static Photo Create(long sourceId, long ownerId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sourceId, nameof(sourceId));
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ownerId, nameof(ownerId));

		return new Photo(sourceId, ownerId);
	}
}
