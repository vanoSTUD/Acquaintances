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
			.HasForeignKey<Profile>(p => p.OwnerId);

		builder.HasMany(p => p.Photos)
			.WithOne(p => p.Profile)
			.HasForeignKey(p => p.ProfileId)
			.OnDelete(DeleteBehavior.Cascade); 

		builder.ComplexProperty(p => p.Name, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName("Name")
				.HasMaxLength(Name.NameLength);
		});
		builder.ComplexProperty(p => p.Description, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName("Description")
				.HasMaxLength(Description.DescriptionLength);
		});
		builder.ComplexProperty(p => p.City, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName("City")
				.HasMaxLength(City.CityLength);
		});
		builder.ComplexProperty(p => p.Age, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value).HasColumnName("Age");
		});
		builder.ComplexProperty(p => p.Gender, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName("Gender")
				.HasMaxLength(Gender.MaxLength);
		});
		builder.ComplexProperty(p => p.PreferredGender, b =>
		{
			b.IsRequired();
			b.Property(n => n.Value)
				.HasColumnName("PreferredGender")
				.HasMaxLength(Gender.MaxLength);
		});

		builder.Property(p => p.Active).HasDefaultValue(true);

		builder.Navigation(p => p.Photos).UsePropertyAccessMode(PropertyAccessMode.Field);
	}
}
