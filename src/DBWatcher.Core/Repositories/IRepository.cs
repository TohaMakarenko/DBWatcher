using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IRepository<T, TKey> where T : BaseDto<TKey>
    {
        Task<T> Get(TKey id);
        Task<T> Update(T entity);
        Task<T> Insert(T entity);
        Task Delete(TKey id);
    }
}