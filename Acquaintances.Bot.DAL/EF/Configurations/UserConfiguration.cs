using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acquaintances.Bot.DAL.EF.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("users");

		builder.HasKey(u => u.Id);
		builder.Property(u => u.Id).ValueGeneratedOnAdd();

		builder.Property(u => u.ChatId).IsRequired();
		builder.HasIndex(u => u.ChatId).IsUnique();

		builder.HasOne(u => u.Profile).WithOne(p => p.Owner).HasForeignKey<User>(u => u.ProfileId);

		builder.HasMany(u => u.AdmirerLikes).WithOne().HasForeignKey(l => l.RecipientId);
		builder.HasMany(u => u.Reciprocities).WithOne().HasForeignKey(l => l.RecipientId);

		builder.Navigation(u => u.AdmirerLikes).UsePropertyAccessMode(PropertyAccessMode.Field);
		builder.Navigation(u => u.Reciprocities).UsePropertyAccessMode(PropertyAccessMode.Field);
	}
}
