using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSingleAsync(object id);
        Task<bool> CreateAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(object id);
    }
}
