namespace Acquaintances.Bot.Domain.Enums;

public enum UserStates
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

	ChangingDescription,
	ChangingPhotos,
	//Чужие профили
	ViewingProfiles,
	CreatingLikeMessage,

	//Изменение в анкете
	EditingProfilePhotos,
	EditingProfileDescription
}
