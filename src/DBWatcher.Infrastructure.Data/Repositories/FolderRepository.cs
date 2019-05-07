using System.Linq;
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
                Builders<Folder>.Update.Pull(x => x.Scripts, scriptId));
        }

        public async Task<Folder> MoveScript(int id, int scriptId)
        {
            // remove from prev folder if it exists and is not destination folder
            await GetCollection()
                .UpdateOneAsync(x => x.Id != id && x.Scripts.Contains(scriptId),
                    Builders<Folder>.Update.Pull(x => x.Scripts, scriptId));

            var newFolder = await GetCollection()
                .FindOneAndUpdateAsync(x => x.Id == id && !x.Scripts.Contains(scriptId),
                    Builders<Folder>.Update
                        .AddToSet(x => x.Scripts, scriptId));
            return newFolder;
        }
    }
}