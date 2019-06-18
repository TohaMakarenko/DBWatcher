using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class DashboardRepository : BaseEventRepository<Dashboard, int>, IDashboardRepository
    {
        public DashboardRepository(IMongoDatabase database) : base(database) { }
        public DashboardRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }

        public override async Task<Dashboard> Insert(Dashboard entity)
        {
            entity.Id = await GetNextId();
            return await base.Insert(entity);
        }
    }
}