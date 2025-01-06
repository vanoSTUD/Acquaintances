namespace Acquaintances.Bot.Application.Abstractions;

public interface IRepository<TEntity> where TEntity : class
{
	Task<TEntity?> GetAsync(long id, CancellationToken ct = default);
	Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct = default);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);

}
