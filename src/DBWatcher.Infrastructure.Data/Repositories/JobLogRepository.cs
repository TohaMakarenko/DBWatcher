using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Dto.Jobs;
using DBWatcher.Core.Repositories;
using DBWatcher.Infrastructure.Data.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class JobLogRepository : BaseEventRepository<JobLog, string>, IJobLogRepository
    {
        private readonly IMapper _mapper;

        public JobLogRepository(IMongoDatabase database, IMapper mapper) : base(database)
        {
            _mapper = mapper;
        }

        public JobLogRepository(IMongoDatabase database, string collectionName, IMapper mapper) : base(database, collectionName)
        {
            _mapper = mapper;
        }

        public override async Task<JobLog> Insert(JobLog entity)
        {
            try {
                entity.Id = ObjectId.GenerateNewId().ToString();
                var mongoEntity = _mapper.Map<JobLogMongo>(entity);
                await Database.GetCollection<JobLogMongo>(nameof(JobLog)).InsertOneAsync(mongoEntity);
            }
            catch (Exception e) { }

            return entity;
        }

        public async Task<IEnumerable<JobLog>> GetForJob(int jobId)
        {
            var entities = await Database.GetCollection<JobLogMongo>(nameof(JobLog))
                .Find(x => x.JobId == jobId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<JobLog>>(entities);
        }

        public async Task<IEnumerable<JobLog>> GetForJob(int jobId, int skip, int take)
        {
            var entities = await Database.GetCollection<JobLogMongo>(nameof(JobLog))
                .Find(x => x.JobId == jobId)
                .Sort(Builders<JobLogMongo>.Sort.Descending(x => x.StartTime))
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
            return _mapper.Map<IEnumerable<JobLog>>(entities);
        }

        public async Task<IEnumerable<JobLog>> Search(JobLogSearchFilter filter)
        {
            if (!filter.Skip.HasValue)
                filter.Skip = 0;
            if (!filter.Take.HasValue)
                filter.Skip = 100;
            var result = await Database.GetCollection<JobLogMongo>(nameof(JobLog))
                .Find(x => x.JobId == filter.JobId
                           && x.Context.ConnectionId == filter.ConnectionId
                           && x.Context.Database == filter.Database)
                .Sort(Builders<JobLogMongo>.Sort.Descending(x => x.StartTime))
                .Skip(filter.Skip)
                .Limit(filter.Take)
                .ToListAsync();
            return _mapper.Map<IEnumerable<JobLog>>(result).OrderBy(x => x.StartTime);
        }
    }
}