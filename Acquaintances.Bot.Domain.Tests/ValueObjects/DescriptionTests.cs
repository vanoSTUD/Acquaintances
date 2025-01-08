using Acquaintances.Bot.Domain.ValueObjects.Profile;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Tests.ValueObjects;

public class DescriptionTests
{
    [Fact]
	public void Equals_EqualsObjects_ShouldReturnTrue()
	{
		// Arrange
		var value = new string('a', Description.MinLength);
		var obj1 = Description.Create(value.ToString()).Value;
		var obj2 = Description.Create(value.ToString()).Value;

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
		var ageValue1 = new string('a', Description.MinLength);
		var ageValue2 = new string('b', Description.MaxLength);
		var obj1 = Description.Create(ageValue1.ToString()).Value;
		var obj2 = Description.Create(ageValue2.ToString()).Value;

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
		var invalidValues = new List<string>
		{
			new ('a', Description.MaxLength + 1)
		};

		// Act
		var results = new List<Result<Description>>();

		foreach (var value in invalidValues)
			results.Add(Description.Create(value));

		// Assert
		Assert.All(results, result => Assert.True(result.IsFailure));
	}

	[Fact]
	public void Create_ValidValues_ShouldReturnSuccess()
	{
		// Arrange
		var validValues = new List<string?>
		{
			new ('a', Description.MaxLength),
			new ('a', Description.MinLength),
			new ('a', Description.MaxLength - Description.MinLength),
			string.Empty,
			null,
			" ",
			""
		};

		// Act
		var results = new List<Result<Description>>();

		foreach (var value in validValues)
			results.Add(Description.Create(value));

		// Assert
		Assert.All(results, result => Assert.True(result.IsSuccess));
	}
}
