using Acquaintances.Bot.Domain.Entities;

namespace Acquaintances.Bot.Domain.Tests.Entities;

public class ReciprocityTests
{
	[Theory]
	[InlineData(-1, 1)]
	[InlineData(1, -1)]
	[InlineData(0, -1)]
	[InlineData(-1, 0)]
	[InlineData(1, 0)]
	[InlineData(0, 1)]
	public void Create_ArgumentsIsNegativeOrZero_ShouldThrowException(long senderId, long recipientId)
	{
		// Assert
		Assert.Throws<ArgumentOutOfRangeException>(() => Reciprocity.Create(senderId, recipientId));
	}

	[Theory]
	[InlineData(1, 1)]
	[InlineData(111, 111)]
	public void Create_SenderIdAndRecipientIdIsEquals_ShouldThrowException(long senderId, long recipientId)
	{
		// Assert
		Assert.Throws<InvalidOperationException>(() => Reciprocity.Create(senderId, recipientId));
	}

	[Fact]
	public void Create_InvalidArguments_ShouldReturnValidObject()
	{
		//Arrange 
		var validRecipientId = 1;
		var validAdmirerId = 2;

		// Act
		var obj = Reciprocity.Create(validRecipientId, validAdmirerId).Value;

		// Assert
		Assert.NotNull(obj);
		Assert.NotEqual(obj.RecipientId, obj.AdmirerId);
		Assert.Equal(obj.RecipientId, validRecipientId);
		Assert.Equal(obj.AdmirerId, validAdmirerId);
	}
}
