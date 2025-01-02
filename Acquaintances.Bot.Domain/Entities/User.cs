using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class User : Entity
{
	private readonly List<Like> _admirerLikes = [];
	private readonly List<Reciprocity> _reciprocities = [];

    private User(long chatId)
    {
        ChatId = chatId;
	}

	// Для EF Core
	private User() { }

	public long ChatId { get; private set; }
	public Profile? Profile { get; private set; }
	public long? ProfileId { get; private set; }
	public IReadOnlyList<Reciprocity> Reciprocities => _reciprocities;
	public IReadOnlyList<Like> AdmirerLikes => _admirerLikes;

	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static User Create(long chatId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(chatId, nameof(chatId));

		return new User(chatId);
	}

	public void SetProfile(Profile? profile)
	{
		Profile = profile;
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
