using System.Linq.Expressions;

namespace backend.Repository;

public interface IRepository<T> where T : class, IEntity
{
    Task CreateAsync(IEnumerable<T> entities);

    Task<IEnumerable<T>> GetAllAsync();

    public Task DeleteManyAsync(Predicate<T> predicate);
}