using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Acquaintances.Bot.DAL.EF;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Profile> Profiles { get; set; }
	public DbSet<Like> Likes { get; set; }
}
