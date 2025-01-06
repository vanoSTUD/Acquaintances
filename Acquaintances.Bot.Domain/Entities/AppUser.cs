using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.Models;
using CSharpFunctionalExtensions;
using System.Text.Json;

namespace Acquaintances.Bot.Domain.Entities;

public class AppUser
{
	private readonly List<Like> _admirerLikes = [];
	private readonly List<Reciprocity> _reciprocities = [];

    private AppUser(long chatId)
    {
        ChatId = chatId;
	}

	// Для EF Core
	private AppUser() { }

	public long ChatId { get; private set; }
	public Profile? Profile { get; private set; }
	public long? ProfileId { get; private set; }
	public State State { get; private set; } = State.None;
	public string? TempDataJson { get; private set; }
	public IReadOnlyList<Reciprocity> Reciprocities => _reciprocities;
	public IReadOnlyList<Like> AdmirerLikes => _admirerLikes;

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static AppUser Create(long chatId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(chatId, nameof(chatId));

		return new AppUser(chatId);
	}

	public TempProfile? GetTempProfile()
	{
		if (TempDataJson == null)
			return null;

		return JsonSerializer.Deserialize<TempProfile>(TempDataJson);
	} 

	public void SetTempProfile(TempProfile? tempProfile)
	{
		if (tempProfile == null)
			TempDataJson = null;

		TempDataJson = JsonSerializer.Serialize(tempProfile);
	}

	public void SetProfile(Profile? profile)
	{
		Profile = profile;
	}

	public void SetState(State state)
	{
		State = state;
	}

	/// <exception cref="ArgumentNullException"></exception>
	public Result AddAdmirerLike(Like like)
	{
		ArgumentNullException.ThrowIfNull(like, nameof(like));

		if (_admirerLikes.Contains(like))
			return Result.Failure($"Попытка добавления дубликата лайка к профилю '{ChatId}'.");

		_admirerLikes.Add(like);
		return Result.Success();
	}

	/// <exception cref="ArgumentNullException"></exception>
	public Result RemoveAdmirerLike(Like like)
	{
		ArgumentNullException.ThrowIfNull(like, nameof(like));

		if (_admirerLikes.Contains(like) == false)
			return Result.Failure("Попытка удаления несуществующего лайка.");

		_admirerLikes.Remove(like);
		return Result.Success();
	}

	/// <exception cref="ArgumentNullException"></exception>
	public Result AddReciprocity(Reciprocity reciprocity)
	{
		ArgumentNullException.ThrowIfNull(reciprocity, nameof(reciprocity));

		if (_reciprocities.Contains(reciprocity))
			return Result.Failure("Попытка дублирования взаимной симпатии.");

		_reciprocities.Add(reciprocity);
		return Result.Success();
	}

	/// <exception cref="ArgumentNullException"></exception>
	public Result RemoveReciprocity(Reciprocity reciprocity)
	{
		ArgumentNullException.ThrowIfNull(reciprocity, nameof(reciprocity));

		if (_reciprocities.Contains(reciprocity) == false)
			return Result.Failure("Попытка удаления несуществующей взаимной симпатии.");

		_reciprocities.Remove(reciprocity);
		return Result.Success();
	}
}
