namespace WAMServer.Interfaces
{
    public interface IRepository<T>
    {
        T? Get(Guid id);
        Task<IEnumerable<T?>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity, Func<T, bool> predicate);
        Task<T?> DeleteAsync(Guid id);
    }
}