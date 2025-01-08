using Acquaintances.Bot.Domain.ValueObjects.Profile;

namespace Acquaintances.Bot.Domain.Tests.ValueObjects
{
	public class AgeTests
	{
		[Fact]
		public void Equals_EqualsObjects_ShouldReturnTrue()
		{
			// Arrange
			var value = Age.MaxAge - Age.MinAge;
			var age1 = Age.Create(value.ToString()).Value;
			var age2 = Age.Create(value.ToString()).Value;

			// Act
			var isEquals1 = age1.Equals(age2);
			var isEquals2 = age1 == age2;

			// Assert
			Assert.True(isEquals1);
			Assert.True(isEquals2);
		}

		[Fact]
		public void Equals_DifferentObjects_ShouldReturnFalse()
		{
			// Arrange
			var ageValue1 = Age.MaxAge - Age.MinAge;
			var ageValue2 = Age.MaxAge - Age.MinAge - 1;
			var age1 = Age.Create(ageValue1.ToString()).Value;
			var age2 = Age.Create(ageValue2.ToString()).Value;

			// Act
			var isEquals1 = age1.Equals(age2);
			var isEquals2 = age1 == age2;

			// Assert
			Assert.False(isEquals1);
			Assert.False(isEquals2);
		}

		[Theory]
		[InlineData(Age.MaxAge + 1)]
		[InlineData(Age.MinAge - 1)]
		[InlineData(null)]
		public void Create_InvalidValues_ShouldReturnFailure(int? inputValue)
		{
			// Arrange
			var value = inputValue?.ToString();

			// Act
			var result = Age.Create(value);

			// Assert
			Assert.True(result.IsFailure);
		}

		[Theory]
		[InlineData(Age.MaxAge)]
		[InlineData(Age.MinAge)]
		[InlineData(Age.MaxAge - 1)]
		[InlineData(Age.MinAge + 1)]
		public void Create_ValidValues_ShouldReturnSuccess(int? inputValue)
		{
			// Arrange
			var value = inputValue?.ToString();

			// Act
			var result = Age.Create(value);

			// Assert
			Assert.True(result.IsSuccess);
		}
	}
}