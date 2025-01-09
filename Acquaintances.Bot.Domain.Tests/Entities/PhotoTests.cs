using Acquaintances.Bot.Domain.Entities;

namespace Acquaintances.Bot.Domain.Tests.Entities;

public class PhotoTests
{
	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(-10000)]
	public void Create_ProfileIdIsNegativeOrZero_ShouldThrowException(long profileId)
	{
		//Arrange
		var fileId = Guid.NewGuid().ToString();

		// Assert
		Assert.Throws<ArgumentOutOfRangeException>(() => Photo.Create(fileId, profileId));
	}

	[Theory]
	[InlineData("      ")]
	[InlineData("")]
	public void Create_FileIdIsInvalid_ShouldReturnFailure(string fileId)
	{
		//Arrange
		var profileId = 1;

		//Act
		var result = Photo.Create(fileId, profileId);

		// Assert
		Assert.True(result.IsFailure);
	}

	[Theory]
	[InlineData("asTdl-df8gd5Faa-dsdHJ-45a", 1)]
	[InlineData("asTdl-df8gdsdHJ-45a", 25)]
	public void Create_InvalidArguments_ShouldReturnValidObject(string fileId, long profileId)
	{
		//Act
		var obj = Photo.Create(fileId, profileId).Value;

		// Assert
		Assert.NotNull(obj);
		Assert.Equal(obj.FileId, fileId);
		Assert.Equal(obj.ProfileId, profileId);
	}
}
