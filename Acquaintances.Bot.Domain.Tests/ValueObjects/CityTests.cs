using Acquaintances.Bot.Domain.ValueObjects.Profile;

namespace Acquaintances.Bot.Domain.Tests.ValueObjects;

public class CityTests
{
	[Fact]
	public void Equals_EqualsObjects_ShouldReturnTrue()
	{
		// Arrange
		var value = "Москва";
		var obj1 = City.Create(value.ToString()).Value;
		var obj2 = City.Create(value.ToString()).Value;

		// Act
		var isEquals1 = obj1.Equals(obj2);
		var isEquals2 = obj1 == obj2;

		// Assert
		Assert.True(isEquals1);
		Assert.True(isEquals2);
	}

	[Fact]
	public void Equals_DifferentObjects_ShouldReturnFalse()
	{
		// Arrange
		var ageValue1 = "Москва";
		var ageValue2 = "Воронеж";
		var obj1 = City.Create(ageValue1.ToString()).Value;
		var obj2 = City.Create(ageValue2.ToString()).Value;

		// Act
		var isEquals1 = obj1.Equals(obj2);
		var isEquals2 = obj1 == obj2;

		// Assert
		Assert.False(isEquals1);
		Assert.False(isEquals2);
	}

	[Theory]
	[InlineData(" ")]
	[InlineData("Краснокаменск-КамчатскийКраснокаменск-Камчатский")]
	[InlineData("Москва1234567890")]
	[InlineData("Москва,?\\!@#$%^&*())_></\"'][{}=+-_")]
	[InlineData(null)]
	public void Create_InvalidValues_ShouldReturnFailure(string? inputValue)
	{
		// Arrange
		var value = inputValue?.ToString();

		// Act
		var result = City.Create(value);

		// Assert
		Assert.True(result.IsFailure);
	}

	[Theory]
	[InlineData("Москва")]
	[InlineData("Moscow")]
	[InlineData("Краснокаменск-Камчатский")]
	[InlineData("Оль'гинка")]
	[InlineData("Оль`гинка")]
	[InlineData("Павловский Посад")]
	[InlineData("Ейск")]
	public void Create_ValidValues_ShouldReturnSuccess(string? inputValue)
	{
		// Arrange
		var value = inputValue?.ToString();

		// Act
		var result = City.Create(value);

		// Assert
		Assert.True(result.IsSuccess);
	}
}
