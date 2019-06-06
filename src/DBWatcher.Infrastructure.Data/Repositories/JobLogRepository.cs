using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class JobLogRepository : BaseEventRepository<JobLog, string>, IJobLogRepository
    {
        public JobLogRepository(IMongoDatabase database) : base(database) { }
        public JobLogRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }
        
        public override async Task<JobLog> Insert(JobLog entity)
        {
            entity.Id = ObjectId.GenerateNewId().ToString();
            return await base.Insert(entity);
        }
    }
}