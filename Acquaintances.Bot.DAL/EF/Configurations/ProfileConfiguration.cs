using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.ValueObjects.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acquaintances.Bot.DAL.EF.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
	public void Configure(EntityTypeBuilder<Profile> builder)
	{
		builder.ToTable("profiles");

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();

		builder.HasOne(p => p.Owner)
			.WithOne(u => u.Profile)
			.HasForeignKey<Profile>(p => p.OwnerId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(p => p.Photos)
			.WithOne(p => p.Profile)
			.HasForeignKey(p => p.ProfileId);

		builder.ComplexProperty(p => p.Name, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName(nameof(Name))
				.HasMaxLength(Name.MaxLength);
		});
		builder.ComplexProperty(p => p.Description, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName(nameof(Description))
				.HasMaxLength(Description.MaxLength);
		});
		builder.ComplexProperty(p => p.City, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName(nameof(City))
				.HasMaxLength(City.MaxLength);
		});
		builder.ComplexProperty(p => p.Age, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value).HasColumnName(nameof(Age));
		});
		builder.ComplexProperty(p => p.Gender, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName(nameof(Gender))
				.HasMaxLength(Gender.MaxLength);
		});
		builder.ComplexProperty(p => p.PreferredGender, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName($"Preferred{nameof(Gender)}")
				.HasMaxLength(Gender.MaxLength);
		});

		builder.Property(p => p.Active);
		builder.HasMany(p => p.AdmirerLikes)
			.WithOne()
			.HasForeignKey(l => l.RecipientId);

		builder.HasMany(p => p.Reciprocities)
			.WithOne()
			.HasForeignKey(l => l.RecipientId);

		builder.Navigation(p => p.AdmirerLikes).UsePropertyAccessMode(PropertyAccessMode.Field);
		builder.Navigation(p => p.Reciprocities).UsePropertyAccessMode(PropertyAccessMode.Field);
		builder.Navigation(p => p.Photos).UsePropertyAccessMode(PropertyAccessMode.Field);
	}
}
