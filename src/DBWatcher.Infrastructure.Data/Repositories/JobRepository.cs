using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class JobRepository : BaseEventRepository<Job, int>, IJobRepository
    {
        public JobRepository(IMongoDatabase database) : base(database) { }
        public JobRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }

        public override async Task<Job> Insert(Job entity)
        {
            entity.Id = await GetNextId<Job>();
            return await base.Insert(entity);
        }
    }
}