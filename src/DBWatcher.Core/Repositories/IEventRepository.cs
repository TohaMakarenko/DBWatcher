using System;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Repositories
{
    public interface IEventRepository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        event Action<T> OnInsert;
        event Action<T> OnUpdate;
        event Action<TKey> OnDelete;
    }
}