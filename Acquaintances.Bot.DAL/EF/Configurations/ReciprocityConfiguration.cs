using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acquaintances.Bot.DAL.EF.Configurations;

public class ReciprocityConfiguration : IEntityTypeConfiguration<Reciprocity>
{
	public void Configure(EntityTypeBuilder<Reciprocity> builder)
	{
		builder.ToTable("reciprocities");

		builder.HasKey(r => r.Id);
		builder.Property(r => r.Id).ValueGeneratedOnAdd();

		builder.Property(r => r.RecipientId).IsRequired();
		builder.HasOne<Profile>()
			.WithMany(p => p.Reciprocities)
			.HasForeignKey(p => p.RecipientId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Property(r => r.AdmirerId).IsRequired();
	}
}
