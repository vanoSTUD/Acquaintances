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


	ViewMyProfile,

	//Изменение в анкете
	ChangingDescription,
	ChangingPhotos,
}
