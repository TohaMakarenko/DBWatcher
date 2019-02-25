using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Repositories
{
    public interface IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Task<T> Get(TKey id);
        Task<T> Update(T entity);
        Task<T> Insert(T entity);
        Task Delete(TKey id);
    }
}