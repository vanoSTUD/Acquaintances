using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.Models;
using Acquaintances.Bot.Domain.ValueObjects.Profile;

namespace Acquaintances.Bot.Domain.Tests.Entities;

public class AppUserTests
{
	[Fact]
	public void Create_ChatIdIsNegativeOrZero_ShouldThrowException()
	{
		// Arrange
		var invalidChatIds = new List<long>
		{
			0, -1, -100
		};

		// Act & Assert
		Assert.All(invalidChatIds, chatId => Assert.Throws<ArgumentOutOfRangeException>(() => AppUser.Create(chatId)));
	}

	[Fact]
	public void Create_ChatIdIsValid_ShouldReturnObject()
	{
		// Arrange
		long validChatId = 123;

		// Act
		var user = AppUser.Create(validChatId);

		// Assert
		Assert.NotNull(user);
		Assert.Equal(validChatId, user.ChatId);
	}

	[Fact]
	public void SetState_ShouldUpdateState()
	{
		// Arrange
		var user = AppUser.Create(123);
		var newState = State.CreatingProfile;

		// Act
		user.SetState(newState);

		// Assert
		Assert.Equal(newState, user.State);
	}

	[Fact]
	public void SetTempProfile_ShouldUpdateTempProfile()
	{
		// Arrange
		var user = AppUser.Create(123);
		var tempProfile = new TempProfile
		{
			Name = Name.Create("Алена").Value,
			Age = Age.Create("25").Value
		};

		// Act
		user.SetTempProfile(tempProfile);

		// Assert
		Assert.NotNull(user.TempProfile);
		Assert.Equal(tempProfile, user.TempProfile);
	}

	[Fact]
	public void SetTempProfile_ShouldAllowNull()
	{
		// Arrange
		var user = AppUser.Create(123);

		// Act
		user.SetTempProfile(null);

		// Assert
		Assert.Null(user.TempProfile);
	}

	[Fact]
	public void SetProfile_ShouldUpdateProfile()
	{
		// Arrange
		var user = AppUser.Create(123);
		var profile = Profile.Create(
			123,
			Name.Create("Алена").Value,
			Description.Create("Тестовое описание").Value,
			City.Create("Москва").Value,
			Age.Create("25").Value,
			Gender.Male,
			Gender.Female,
			[Photo.Create("fileId1", 123).Value]
		).Value;

		// Act
		user.SetProfile(profile);

		// Assert
		Assert.NotNull(user.Profile);
		Assert.Equal(profile, user.Profile);
	}

	[Fact]
	public void SetProfile_ShouldAllowNull()
	{
		// Arrange
		var user = AppUser.Create(123);

		// Act
		user.SetProfile(null);

		// Assert
		Assert.Null(user.Profile);
	}
}
