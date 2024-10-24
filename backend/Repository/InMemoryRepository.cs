using System.Linq.Expressions;

namespace backend.Repository;

public class InMemoryRepository<T> : IRepository<T> where T: class, IEntity
{
    private readonly List<T> _store = new List<T>();

    public Task CreateAsync(IEnumerable<T> entities)
    {
        _store.AddRange(entities);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(_store.AsEnumerable());
    }

    public Task DeleteManyAsync(Predicate<T> predicate)
    {
        return Task.FromResult(_store.RemoveAll(predicate));
    }
}