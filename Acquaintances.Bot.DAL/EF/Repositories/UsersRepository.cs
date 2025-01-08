using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acquaintances.Bot.DAL.EF.Repositories;

public class UsersRepository : IRepository<AppUser>
{
	private readonly AppDbContext _db;

	public UsersRepository(AppDbContext db)
	{
		_db = db;
	}

	public async Task<AppUser> CreateAsync(AppUser entity, CancellationToken ct = default)
	{
		ArgumentNullException.ThrowIfNull(entity, nameof(entity));

		var foundUser = _db.Users.Find(entity.ChatId);

		if (foundUser != null)
			return foundUser;

		_db.Add(entity);
		await _db.SaveChangesAsync(ct);

		return entity;
	}

	public async Task<AppUser?> GetAsync(long id, CancellationToken ct = default)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

		return await GetUserAsync(id, ct);
	}

	public async Task RemoveAsync(AppUser entity, CancellationToken ct) 
	{ 
		if (_db.Users.Find(entity.ChatId) == null)
			return;

		_db.Remove(entity);
		await _db.SaveChangesAsync(ct);
	}

	public async Task<AppUser> UpdateAsync(AppUser entity, CancellationToken ct = default)
	{
		ArgumentNullException.ThrowIfNull(entity, nameof(entity));

		_db.Update(entity);
		await _db.SaveChangesAsync(ct);

		return entity;
	}

	private async Task<AppUser?> GetUserAsync(long chatId, CancellationToken ct = default)
	{
		var foundUser = await _db.Users
			.Include(u => u.Profile)
				.ThenInclude(p => p.Photos)
			.Include(u => u.Profile)
				.ThenInclude(p => p.AdmirerLikes)
			.Include(u => u.Profile)
				.ThenInclude(p => p.Reciprocities)
			.FirstOrDefaultAsync(u => u.ChatId.Equals(chatId), ct);

		return foundUser;
	}
}
