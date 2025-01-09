using CSharpFunctionalExtensions;
using System.Net.Http.Headers;
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

	[JsonIgnore]
	public Profile Profile { get; private set; } = null!;

	/// <summary>
	/// Id файла на серверах Telegram
	/// </summary>
	[JsonInclude]
	public string FileId { get; private set; } = null!;

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="ArgumentNullException"></exception>
	public static Result<Photo> Create(string fileId, long profileId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(profileId, nameof(profileId));

		if (string.IsNullOrEmpty(fileId) || fileId.Trim().Length == 0)
			return Result.Failure<Photo>("Не удалось добавить фотографию.");

		return new Photo(fileId, profileId);
	}
}
