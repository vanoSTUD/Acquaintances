using Acquaintances.Bot.Domain.ValueObjects.Profile;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Tests.ValueObjects;

public class GenderTests
{
	[Fact]
	public void Equals_EqualsObjects_ShouldReturnTrue()
	{
		// Arrange
		var obj1 = Gender.Male;
		var obj2 = Gender.Male;

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
		var obj1 = Gender.Male;
		var obj2 = Gender.Female;

		// Act
		var isEquals1 = obj1.Equals(obj2);
		var isEquals2 = obj1 == obj2;

		// Assert
		Assert.False(isEquals1);
		Assert.False(isEquals2);
	}

	[Fact]
	public void Create_InvalidValues_ShouldReturnFailure()
	{
		// Arrange
		var invalidValues = new List<string?>
		{
			Gender.Male.Value + "g",
			Gender.Female.Value + "в",
			Gender.All.Value + "]",
			null,
			string.Empty,
			""
		};

		// Act
		var results = new List<Result<Gender>>();

		foreach (var value in invalidValues)
			results.Add(Gender.Create(value, true));

		// Assert
		Assert.All(results, result => Assert.True(result.IsFailure));
	}

	[Fact]
	public void Create_ValidValues_ShouldReturnSuccess()
	{
		// Arrange
		var validValues = new List<string>
		{
			Gender.Male.Value,
			Gender.Female.Value,
			Gender.All.Value
		};

		// Act
		var results = new List<Result<Gender>>();

		foreach (var value in validValues)
			results.Add(Gender.Create(value, true));

		// Assert
		Assert.All(results, result => Assert.True(result.IsSuccess));
	}
}
