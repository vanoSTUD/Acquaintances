using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acquaintances.Bot.DAL.EF.Repositories;

public class ProfilesReposirory : IRepository<Profile>
{
	private readonly AppDbContext _db;

	public ProfilesReposirory(AppDbContext db)
	{
		_db = db;
	}

	public async Task<Profile> CreateAsync(Profile entity, CancellationToken ct = default)
	{
		var foundedProfile = await GetProfileAsync(entity.Id);

		if (foundedProfile != null)
			return foundedProfile;

		_db.Add(entity);
		await _db.SaveChangesAsync(ct);
		return entity;
	}

	public async Task<Profile?> GetAsync(long id, CancellationToken ct = default)
	{
		return await GetProfileAsync(id);
	}

	public async Task RemoveAsync(Profile entity, CancellationToken ct = default)
	{
		if (_db.Profiles.Find(entity.Id) == null)
			return;

		_db.Remove(entity);
		await _db.SaveChangesAsync(ct);
	}

	public Task<Profile> UpdateAsync(Profile entity, CancellationToken ct = default)
	{
		throw new NotImplementedException();
	}

	private async Task<Profile?> GetProfileAsync(long id)
	{
		return await _db.Profiles.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
	}
}
