using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

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
			.HasForeignKey<AppUser>(u => u.ProfileId);

		builder.Property(u => u.State)
			.HasConversion(
				state => state.ToString(),  // Преобразование Enum -> String
				state => (UserStates)Enum.Parse(typeof(UserStates), state) // Преобразование String -> Enum
			);

		builder.Property(u => u.TempProfile)
			.HasConversion(
				profileJson => JsonSerializer.Serialize(profileJson, (JsonSerializerOptions?)null),
				profile => JsonSerializer.Deserialize<TempProfile>(profile, (JsonSerializerOptions?)null)
			);
	}
}
