using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acquaintances.Bot.DAL.EF.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder)
	{
		builder.ToTable("users");

		builder.HasKey(u => u.ChatId);
		builder.Property(u => u.ChatId).ValueGeneratedNever();
		
		builder.HasOne(u => u.Profile)
			.WithOne(p => p.Owner)
			.HasForeignKey<AppUser>(u => u.ProfileId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Property(u => u.State)
			.HasConversion(
				state => state.ToString(),        // Преобразование Enum -> String
				state => (State)Enum.Parse(typeof(State), state) // Преобразование String -> Enum
			);
		builder.Property(u => u.TempDataJson);

		builder.HasMany(u => u.AdmirerLikes)
			.WithOne()
			.HasForeignKey(l => l.RecipientId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(u => u.Reciprocities)
			.WithOne()
			.HasForeignKey(l => l.RecipientId)
			.OnDelete(DeleteBehavior.Cascade); 

		builder.Navigation(u => u.AdmirerLikes).UsePropertyAccessMode(PropertyAccessMode.Field);
		builder.Navigation(u => u.Reciprocities).UsePropertyAccessMode(PropertyAccessMode.Field);
	}
}
