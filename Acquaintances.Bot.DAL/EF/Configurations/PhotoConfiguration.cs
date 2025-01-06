using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acquaintances.Bot.DAL.EF.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
	public void Configure(EntityTypeBuilder<Photo> builder)
	{
		builder.ToTable("photos");

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();

		builder.Property(p => p.ProfileId).IsRequired();
		builder.HasOne(p => p.Profile).WithMany(p => p.Photos).HasForeignKey(p => p.ProfileId);

		builder.Property(p => p.FileId).IsRequired();
	}
}
