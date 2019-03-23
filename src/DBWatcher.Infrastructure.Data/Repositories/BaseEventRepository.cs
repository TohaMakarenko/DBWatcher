using System;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class BaseEventRepository<T, TKey> : BaseRepository<T, TKey>, IEventRepository<T, TKey>
        where T : BaseDto<TKey>
    {
        public BaseEventRepository(IMongoDatabase database, string collectionName) :
            base(database, collectionName) { }

        public override async Task<T> Insert(T entity)
        {
            var result = await base.Insert(entity);
            OnInsert(entity);
            return result;
        }

        public override async Task<T> Update(T entity)
        {
            var result = await base.Update(entity);
            OnUpdate(entity);
            return result;
        }

        public override async Task Delete(TKey id)
        {
            await base.Delete(id);
            OnDelete(id);
        }

        public event Action<T> OnInsert = x => { };
        public event Action<T> OnUpdate = x => { };
        public event Action<TKey> OnDelete = x => { };
    }
}