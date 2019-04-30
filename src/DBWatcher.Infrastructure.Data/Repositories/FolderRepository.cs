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
    }
}