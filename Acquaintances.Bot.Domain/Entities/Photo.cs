using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.Entities;

public class Photo : Entity
{
	protected Photo(string sourceId, long profileId)
	{
		FileId = sourceId;
		ProfileId = profileId;
	}

	// Для EF Core
	[JsonConstructor]
	protected Photo() { }

	[JsonInclude]
	public long ProfileId { get; private set; }
	[JsonInclude]
	public string FileId { get; private set; } = null!;
	public Profile Profile { get; private set; } = null!;

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public static Result<Photo> Create(string sourceId, long profileId)
	{
		ArgumentException.ThrowIfNullOrEmpty(sourceId, nameof(sourceId));
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(profileId, nameof(profileId));

		return new Photo(sourceId, profileId);
	}
}
