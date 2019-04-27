using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class FolderRepository : BaseRepository<Folder, int>, IFolderRepository
    {
        public FolderRepository(IMongoDatabase database) : base(database) { }
        public FolderRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }
    }
}