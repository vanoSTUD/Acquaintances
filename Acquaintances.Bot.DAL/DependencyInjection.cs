using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.DAL.EF;
using Acquaintances.Bot.DAL.EF.Repositories;
using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Acquaintances.Bot.DAL;

public static class DependencyInjection
{
	public static void AddMSSQLDbContext(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<AppDbContext>(option =>
		{
			option.UseSqlServer(connectionString);
		});

		AddRepositories(services);
	}

	private static void AddRepositories(IServiceCollection services)
	{
		services.AddScoped<IRepository<AppUser>, UserRepository>();
		services.AddScoped<IRepository<Profile>, ProfileReposirory>();
	}
}
