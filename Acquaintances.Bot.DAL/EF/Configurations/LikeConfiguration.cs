using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acquaintances.Bot.DAL.EF.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
	public void Configure(EntityTypeBuilder<Like> builder)
	{
		builder.ToTable("likes");

		builder.HasKey(l => l.Id);
		builder.Property(l => l.Id).ValueGeneratedOnAdd();

		builder.HasOne<AppUser>()
			.WithMany()
			.HasForeignKey(l => l.RecipientId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Property(l => l.Message).HasMaxLength(Like.MaxMessageLength);

		builder.Property(l => l.SenderId).IsRequired();
	}
}
