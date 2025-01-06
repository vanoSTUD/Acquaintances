using Acquaintances.Bot.Domain.ValueObjects.Profile;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

/// <summary>
/// Анкета
/// </summary>
public class Profile : Entity
{
	public const int MaxPhotos = 3;
	private readonly List<Photo> _photos = null!;

	private Profile(long ownerId, Name name, Description description, City city, Age age, Gender gender, Gender preferredGender, List<Photo> photos)
	{
		Age = age;
		Name = name;
		City = city;
		Gender = gender;
		OwnerId = ownerId;
		Description = description;
		PreferredGender = preferredGender;
		Active = true;
		_photos = photos;
	}

	// Для EF Core
	private Profile() { }

	/// <summary>
	/// Id владельца профиля - AppUser
	/// </summary>
	public long OwnerId { get; private set; }
	public AppUser Owner { get; private set; } = null!;

	/// <summary>
	/// Имя в анкете
	/// </summary>
	public Name Name { get; private set; } = null!;

	/// <summary>
	/// Описание анкеты
	/// </summary>
	public Description Description { get; private set; } = null!;

	/// <summary>
	/// Город анкеты
	/// </summary>
	public City City { get; private set; } = null!;

	/// <summary>
	/// Возраст в анкете
	/// </summary>
	public Age Age { get; private set; } = null!;

	/// <summary>
	/// Пол владельца анкеты
	/// </summary>
	public Gender Gender { get; private set; } = null!;

	/// <summary>
	/// Предпочитаемый пол
	/// </summary>
	public Gender PreferredGender { get; private set; } = null!;

	/// <summary>
	/// Активна ли анкета в данный момент
	/// </summary>
	public bool Active { get; private set; }

	/// <summary>
	/// Фотографии профиля
	/// </summary>
	public IReadOnlyList<Photo> Photos => _photos;


	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static Result<Profile> Create(
		long ownerId,
		Name name,
		Description description,
		City city,
		Age age,
		Gender gender,
		Gender preferredGender,
		List<Photo> photos)
	{
		ArgumentNullException.ThrowIfNull(age, nameof(age));
		ArgumentNullException.ThrowIfNull(name, nameof(name));
		ArgumentNullException.ThrowIfNull(city, nameof(city));
		ArgumentNullException.ThrowIfNull(gender, nameof(gender));
		ArgumentNullException.ThrowIfNull(description, nameof(description));
		ArgumentNullException.ThrowIfNull(preferredGender, nameof(preferredGender));
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ownerId, nameof(ownerId));

		if (photos == null || photos.Count == 0)
			return Result.Failure<Profile>("Отсутствуют фотографии профиля");

		if (photos.Count > MaxPhotos)
			photos = photos.Take(MaxPhotos).ToList();

		return new Profile(ownerId, name, description, city, age, gender, preferredGender, photos);
	}

	public void SetActive(bool active) 
	{
		Active = active;
	}

	public string GetFullCaption()
	{
		return $"{Name}, {Age}, {City} - {Description}";
	}
}
