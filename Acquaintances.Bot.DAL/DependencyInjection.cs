using Acquaintances.Bot.DAL.EF;
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
	}
}
