using Acquaintances.Bot.Domain.ValueObjects.Profile;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class Profile : Entity
{
	public const int MaxPhotos = 3;
	private List<Photo> _photos = null!;

	private Profile(User owner, Name name, Description description, City city, Age age, Gender gender, Gender preferredGender, List<Photo> photos)
	{
		Age = age;
		Name = name;
		City = city;
		Owner = owner;
		Gender = gender;
		Description = description;
		PreferredGender = preferredGender;
		Active = true;
		_photos = photos;
	}

	// Для EF Core
	private Profile() { }

	public User Owner { get; private set; } = null!;
	public Name Name { get; private set; } = null!;
	public Description Description { get; private set; } = null!;
	public City City { get; private set; } = null!;
	public Age Age { get; private set; } = null!;
	public Gender Gender { get; private set; } = null!;
	public Gender PreferredGender { get; private set; } = null!;
	public bool Active { get; private set; }
	public IReadOnlyList<Photo> Photos => _photos;

	/// <exception cref="ArgumentNullException"></exception>
	public static Result<Profile> Create(
		User owner,
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
		ArgumentNullException.ThrowIfNull(owner, nameof(owner));
		ArgumentNullException.ThrowIfNull(gender, nameof(gender));
		ArgumentNullException.ThrowIfNull(description, nameof(description));
		ArgumentNullException.ThrowIfNull(preferredGender, nameof(preferredGender));

		if (photos == null || photos.Count == 0)
			return Result.Failure<Profile>("Отсутствуют фотографии профиля");

		if (photos.Count > MaxPhotos)
			photos = photos.Take(MaxPhotos).ToList();

		return new Profile(owner, name, description, city, age, gender, preferredGender, photos);
	}

	public void SetActive(bool active) 
	{
		Active = active;
	}
}
