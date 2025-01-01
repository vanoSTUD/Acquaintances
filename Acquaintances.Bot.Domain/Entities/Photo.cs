using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class Photo : Entity
{ 
	private Photo(long sourceId, long ownerId)
	{
		SourceId = sourceId;
		OwnerId = ownerId;
	}

	public long SourceId { get; init; }
	public long OwnerId { get; init; }

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static Photo Create(long sourceId, long ownerId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sourceId, nameof(sourceId));
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ownerId, nameof(ownerId));

		return new Photo(sourceId, ownerId);
	}
}
