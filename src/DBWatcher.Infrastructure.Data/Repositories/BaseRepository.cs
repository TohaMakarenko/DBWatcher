using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class BaseRepository<T, TKey> : IRepository<T, TKey> where T : BaseDto<TKey>
    {
        protected readonly string CollectionName;
        protected readonly IMongoDatabase Database;

        public BaseRepository(IMongoDatabase database)
        {
            Database = database;
        }

        public BaseRepository(IMongoDatabase database, string collectionName)
        {
            Database = database;
            CollectionName = collectionName;
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

        protected IMongoCollection<T> GetCollection()
        {
            return string.IsNullOrEmpty(CollectionName)
                ? Database.GetCollection<T>()
                : Database.GetCollection<T>(CollectionName);
        }

        private FilterDefinition<T> GetIdFilter(TKey id)
        {
            return Builders<T>.Filter.Eq(x => x.Id, id);
        }

        public async Task<int> GetNextId<T>()
        {
            var update = Builders<Sequence>.Update.Inc(x => x.Value, 1);
            var options = new FindOneAndUpdateOptions<Sequence> {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };
            var value = await Database.GetCollection<Sequence>()
                .FindOneAndUpdateAsync<Sequence>(x => x.Name == typeof(T).Name, update, options);
            return value.Value;
        }
    }
}