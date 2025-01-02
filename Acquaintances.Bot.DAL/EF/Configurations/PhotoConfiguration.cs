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

		builder.Property(p => p.OwnerId).IsRequired();
		builder.HasOne<User>().WithMany().HasForeignKey(p => p.OwnerId);

		builder.Property(p => p.SourceId).IsRequired();
	}
}
