using WAMServer.DTO;
using WAMServer.Models;

namespace WAMServer.Interfaces
{
    public interface IControlPC<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}