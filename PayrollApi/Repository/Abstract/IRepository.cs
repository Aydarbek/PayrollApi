using PayrollApi.Models;

namespace PayrollApi.Repository.Abstract;

public interface IRepository<TEntity> : IAsyncDisposable
{
    Task<TEntity> Get(long id, CancellationToken token);
    Task<List<TEntity>> GetAll(CancellationToken token);
    Task Add(TEntity entity, CancellationToken token);
    void Update(TEntity entity);
    Task Save(CancellationToken token);
}