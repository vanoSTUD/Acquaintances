using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.Models;

namespace Acquaintances.Bot.Domain.Entities;

/// <summary>
/// Пользователь бота
/// </summary>
public class AppUser
{
	private AppUser(long chatId)
	{
		ChatId = chatId;
	}

	// Для EF Core
	private AppUser() { }

	/// <summary>
	/// Id чата в телеграме. Уникален при личной переписке
	/// </summary>
	public long ChatId { get; private set; }

	/// <summary>
	/// Анкета пользователя
	/// </summary>
	public Profile? Profile { get; private set; }
	public long? ProfileId { get; private set; }

	/// <summary>
	/// Состояние в котором прибывает пользователь, к примеру создание анкеты
	/// </summary>
	public State State { get; private set; } = State.None;

	/// <summary>
	/// Временные данные, например данные создания анкеты
	/// </summary>
	public TempProfile? TempProfile { get; private set; }


	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static AppUser Create(long chatId)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(chatId, nameof(chatId));

		return new AppUser(chatId);
	}

	public void SetTempProfile(TempProfile? tempProfile)
	{
		TempProfile = tempProfile;
	}

	public void SetProfile(Profile? profile)
	{
		Profile = profile;
	}

	public void SetState(State state)
	{
		State = state;
	}
}
