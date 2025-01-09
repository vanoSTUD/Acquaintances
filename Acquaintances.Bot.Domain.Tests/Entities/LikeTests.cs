using Acquaintances.Bot.Domain.Entities;

namespace Acquaintances.Bot.Domain.Tests.Entities;

public class LikeTests
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
		Assert.Throws<ArgumentOutOfRangeException>(() => Like.Create(senderId, recipientId));
	}

	[Theory]
	[InlineData(1, 1)]
	[InlineData(111, 111)]
	public void Create_SenderIdAndRecipientIdIsEquals_ShouldThrowException(long senderId, long recipientId)
	{
		// Assert
		Assert.Throws<InvalidOperationException>(() => Like.Create(senderId, recipientId));
	}

	[Fact]
	public void Create_MessageIsInvalid_ShouldReturnFailure()
	{
		//Arrange 
		var validSenderId = 1;
		var validRecipientId = 2;
		var invalidMessages = new List<string>
		{
			new('a', Like.MaxMessageLength + 1),
			new('a', Like.MinMessageLength - 1),
		};

		// Act
		var results = invalidMessages.Select(message => Like.Create(validSenderId, validRecipientId, message));

		// Assert
		Assert.All(results, result => Assert.True(result.IsFailure));
	}

	[Fact]
	public void Create_MessageIsValid_ShouldReturnSuccess()
	{
		//Arrange 
		var validSenderId = 1;
		var validRecipientId = 2;
		var validMessages = new List<string>
		{
			new('a', Like.MaxMessageLength),
			new('a', Like.MinMessageLength),
			new('a', Like.MaxMessageLength - Like.MinMessageLength),
		};

		// Act
		var results = validMessages.Select(message => Like.Create(validSenderId, validRecipientId, message));

		// Assert
		Assert.All(results, result => Assert.True(result.IsSuccess));
	}

	[Fact]
	public void Create_WithNotNullMessage_ShouldReturnValidObject()
	{
		//Arrange 
		var validSenderId = 1;
		var validRecipientId = 2;
		var validMessage = new string('a', Like.MaxMessageLength);

		// Act
		var obj = Like.Create(validSenderId, validRecipientId, validMessage).Value;

		// Assert
		Assert.NotNull(obj);
		Assert.Equal(obj.Message, validMessage);
		Assert.Equal(obj.SenderId, validSenderId);
		Assert.Equal(obj.RecipientId, validRecipientId);
	}
}
