using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.ValueObjects.Profile;

namespace Acquaintances.Bot.Domain.Tests.Entities;

public class ProfileTests
{
	[Fact]
	public void Create_ValidArguments_ShouldReturnValidObject()
	{
		//Arrange
		var ownerId = 123;
		var name = Name.Create("Тестовое имя").Value;
		var description = Description.Create("Тестовое описание").Value;
		var city = City.Create("Москва").Value;
		var age = Age.Create("25").Value;
		var gender = Gender.Male;
		var preferredGender = Gender.Female;
		var photos = Enumerable.Range(1, Profile.MaxPhotos)
				.Select(i => Photo.Create(Guid.NewGuid().ToString(), i).Value)
				.ToList();

		// Act
		var profileResult = Profile.Create(ownerId, name, description, city, age, gender, preferredGender, photos);

		// Assert
		Assert.True(profileResult.IsSuccess);
		Assert.NotNull(profileResult.Value);
		Assert.Equal(ownerId, profileResult.Value.OwnerId);
		Assert.Equal(name, profileResult.Value.Name);
		Assert.Equal(description, profileResult.Value.Description);
		Assert.Equal(city, profileResult.Value.City);
		Assert.Equal(age, profileResult.Value.Age);
		Assert.Equal(gender, profileResult.Value.Gender);
		Assert.Equal(preferredGender, profileResult.Value.PreferredGender);
		Assert.Equal(photos.Count, profileResult.Value.Photos.Count);
	}

	[Fact]
	public void Create_PhotoOverload_ShouldOverloadPhotosToMaxPhotos()
	{
		// Act
		var result = Profile.Create(
			123,
			Name.Create("тестовое имя").Value,
			Description.Create("Тестовое описание").Value,
			City.Create("Москва").Value,
			Age.Create("25").Value,
			Gender.Male,
			Gender.Female,
			[
				Photo.Create(Guid.NewGuid().ToString(), 1).Value,
				Photo.Create(Guid.NewGuid().ToString(), 1).Value,
				Photo.Create(Guid.NewGuid().ToString(), 1).Value,
				Photo.Create(Guid.NewGuid().ToString(), 1).Value
			]
		);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(Profile.MaxPhotos, result.Value.Photos.Count);
	}

	[Fact]
	public void AddAdmirerLike_ValidLike_ShouldAddLike()
	{
		// Arrange
		var profile = CreateValidProfile();
		var like = Like.Create(456, profile.OwnerId).Value;

		// Act
		var result = profile.AddAdmirerLike(like);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Contains(like, profile.AdmirerLikes);
	}

	[Fact]
	public void AddAdmirerLike_DuplicateLike_ShouldNotAddLike()
	{
		// Arrange
		var profile = CreateValidProfile();
		var like = Like.Create(456, profile.OwnerId).Value;
		profile.AddAdmirerLike(like);

		// Act
		var result = profile.AddAdmirerLike(like);
		var distinctCount = profile.AdmirerLikes.Distinct().Count();

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(profile.AdmirerLikes.Count, distinctCount);
	}

	[Fact]
	public void RemoveAdmirerLike_ValidLike_ShouldRemoveLike()
	{
		// Arrange
		var profile = CreateValidProfile();
		var like = Like.Create(456, profile.OwnerId).Value;
		profile.AddAdmirerLike(like);

		// Act
		var result = profile.RemoveAdmirerLike(like);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.DoesNotContain(like, profile.AdmirerLikes);
	}

	[Fact]
	public void RemoveAdmirerLike_LikeDoesNotExist_ShouldReturnFailure()
	{
		// Arrange
		var profile = CreateValidProfile();
		var likes = profile.AdmirerLikes;
		var like = Like.Create(1111111111, profile.OwnerId).Value;

		// Act
		var result = profile.RemoveAdmirerLike(like);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(profile.AdmirerLikes, likes);
	}

	[Fact]
	public void SetActive_ShouldUpdateActiveStatus()
	{
		// Arrange
		var profile = CreateValidProfile();

		// Act
		profile.SetActive(false);
		var result1 = profile.Active;
		profile.SetActive(true);
		var result2 = profile.Active;

		// Assert
		Assert.False(result1);
		Assert.True(result2);
	}

	[Fact]
	public void GetFullCaption_ShouldReturnFormattedCaption()
	{
		// Arrange
		var profile = CreateValidProfile();

		// Act
		var caption = profile.GetFullCaption();

		// Assert
		Assert.NotEmpty(caption);
		Assert.NotNull(caption);
	}

	private static Profile CreateValidProfile()
	{
		return Profile.Create(
			123,
			Name.Create("тестовое имя").Value,
			Description.Create("Тестовое описание").Value,
			City.Create("Москва").Value,
			Age.Create("25").Value,
			Gender.Male,
			Gender.Female,
			Enumerable.Range(1, Profile.MaxPhotos)
				.Select(i => Photo.Create(Guid.NewGuid().ToString(), i).Value)
				.ToList()
		).Value;
	}
}
