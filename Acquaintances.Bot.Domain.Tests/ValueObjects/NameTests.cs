using Acquaintances.Bot.Domain.ValueObjects.Profile;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Domain.Tests.ValueObjects;

public class NameTests
{
	[Fact]
	public void Equals_EqualsObjects_ShouldReturnTrue()
	{
		// Arrange
		var value = "Alena";
		var obj1 = Name.Create(value).Value;
		var obj2 = Name.Create(value).Value;

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
		var obj1 = Name.Create("Aleksy").Value;
		var obj2 = Name.Create("Ivan").Value;

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
			null,
			string.Empty,
			"",
			new ('a', Name.MaxLength + 1),
			new ('a', Name.MinLength - 1)
		};

		// Act
		var results = new List<Result<Name>>();

		foreach (var value in invalidValues)
			results.Add(Name.Create(value));

		// Assert
		Assert.All(results, result => Assert.True(result.IsFailure));
	}

	[Fact]
	public void Create_ValidValues_ShouldReturnSuccess()
	{
		// Arrange
		var validValues = new List<string>
		{
			new ('г', Name.MaxLength),
			new ('b', Name.MinLength),
			"Ivan228",
			"Аленка❤️"
		};

		// Act
		var results = new List<Result<Name>>();

		foreach (var value in validValues)
			results.Add(Name.Create(value));

		// Assert
		Assert.All(results, result => Assert.True(result.IsSuccess));
	}
}
