using Acquaintances.Bot.Domain.ValueObjects.Profile;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Entities;

public class Profile : Entity
{
	public const int MaxPhotos = 3;
	private readonly List<Photo> _images;

	private Profile(ProfileName name, ProfileDescription description, ProfileCity city, ProfileAge age, List<Photo> images)
	{
		Name = name;
		Description = description;
		City = city;
		Age = age;
		_images = images;
	}

	public ProfileName Name { get; private set; }
	public ProfileDescription Description { get; private set; }
	public ProfileCity City { get; private set; }
	public ProfileAge Age { get; private set; }
	public IReadOnlyList<Photo> Images => _images;

	/// <exception cref="ArgumentNullException"></exception>
	public static Result<Profile> Create(ProfileName name, ProfileDescription description, ProfileCity city, ProfileAge age, List<Photo> images)
	{
		ArgumentNullException.ThrowIfNull(name, nameof(name));
		ArgumentNullException.ThrowIfNull(description, nameof(description));
		ArgumentNullException.ThrowIfNull(city, nameof(city));
		ArgumentNullException.ThrowIfNull(age, nameof(age));

		if (images == null || images.Count == 0)
			return Result.Failure<Profile>("Отсутствуют фотографии профиля");

		if (images.Count > MaxPhotos)
			images = images.Take(MaxPhotos).ToList();

		return new Profile(name, description, city, age, images);
	}


}
