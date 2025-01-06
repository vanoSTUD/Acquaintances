using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.Entities;

/// <summary>
/// Фотограя
/// </summary>
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

	/// <summary>
	/// Id владельца фотографий - Profile
	/// </summary>
	[JsonInclude]
	public long ProfileId { get; private set; }
	public Profile Profile { get; private set; } = null!;

	/// <summary>
	/// Id файла на серверах Telegram
	/// </summary>
	[JsonInclude]
	public string FileId { get; private set; } = null!;

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public static Result<Photo> Create(string sourceId, long profileId)
	{
		ArgumentException.ThrowIfNullOrEmpty(sourceId, nameof(sourceId));
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(profileId, nameof(profileId));

		return new Photo(sourceId, profileId);
	}
}
