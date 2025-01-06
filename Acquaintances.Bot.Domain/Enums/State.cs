namespace Acquaintances.Bot.Domain.Enums;

public enum State
{
	None,
	// Создание/Пересоздание анкеты
	CreatingProfile,
	EnteringName,
	EnteringAge,
	EnteringCity,
	EnteringGender,
	EnteringPrefferedGender,
	EnteringDescription,
	SendingPhotos,
	SaveProfile,

	//Чужие профили
	ViewingProfiles,
	CreatingLikeMessage,

	//Изменение в анкете
	EditingProfilePhotos,
	EditingProfileDescription
}
