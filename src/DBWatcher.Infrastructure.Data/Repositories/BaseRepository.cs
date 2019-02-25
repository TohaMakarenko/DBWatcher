using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        protected readonly IMongoDatabase Database;
        protected readonly string CollectionName;

        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            Database = database;
            CollectionName = collectionName;
        }

        protected IMongoCollection<T> GetCollection()
        {
            return Database.GetCollection<T>(CollectionName);
        }

        private FilterDefinition<T> GetIdFilter(TKey id)
        {
            return Builders<T>.Filter.Eq(x => x.Id, id);
        }

        public virtual Task<T> Get(TKey id)
        {
            return GetCollection().Find(GetIdFilter(id)).FirstOrDefaultAsync();
        }

        public virtual async Task<T> Update(T entity)
        {
            await GetCollection().ReplaceOneAsync(GetIdFilter(entity.Id), entity);
            return entity;
        }

        public virtual async Task<T> Insert(T entity)
        {
            await GetCollection().InsertOneAsync(entity);
            return entity;
        }

        public virtual Task Delete(TKey id)
        {
            return GetCollection().DeleteOneAsync(GetIdFilter(id));
        }
    }
}