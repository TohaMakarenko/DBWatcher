using System;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IEventRepository<T, TKey> : IRepository<T, TKey> where T : BaseDto<TKey>
    {
        event Action<T> OnInsert;
        event Action<T> OnUpdate;
        event Action<TKey> OnDelete;
    }
}