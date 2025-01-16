using Acquaintances.Bot.Domain.Entities;

namespace Acquaintances.Bot.Application.Abstractions;

public interface IUsersRepository
{
    Task<AppUser> CreateAsync(AppUser entity, CancellationToken ct = default);
    Task<AppUser?> GetAsync(long chatId, CancellationToken ct = default);
    Task RemoveAsync(AppUser entity, CancellationToken ct);
    Task<AppUser> UpdateAsync(AppUser entity, CancellationToken ct = default);
}