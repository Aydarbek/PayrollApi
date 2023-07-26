namespace PayrollApi.Repository.Abstract;

public interface IRepository<TEntity> : IAsyncDisposable
{
    Task<TEntity> Get(long id, CancellationToken token = default);
    Task<List<TEntity>> GetAll(CancellationToken token = default);
    Task Add(TEntity entity, CancellationToken token = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Save(CancellationToken token = default);
}