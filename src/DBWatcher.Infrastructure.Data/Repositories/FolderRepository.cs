using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class FolderRepository : BaseRepository<Folder, int>, IFolderRepository
    {
        public FolderRepository(IMongoDatabase database) : base(database) { }
        public FolderRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }

        public override async Task<Folder> Insert(Folder entity)
        {
            entity.Id = await GetNextId();
            return await base.Insert(entity);
        }

        public Task<Folder> AddScript(int id, int scriptId)
        {
            return GetCollection().FindOneAndUpdateAsync(x => x.Id == id,
                Builders<Folder>.Update
                    .AddToSet(x => x.Scripts, scriptId));
        }

        public Task<Folder> RemoveScript(int id, int scriptId)
        {
            return GetCollection().FindOneAndUpdateAsync(x => x.Id == id,
                Builders<Folder>.Update
                    .PullFilter(x => x.Scripts, s => s == scriptId));
        }
    }
}