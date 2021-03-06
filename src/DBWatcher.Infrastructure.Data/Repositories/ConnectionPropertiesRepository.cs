﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class ConnectionPropertiesRepository : BaseEventRepository<ConnectionProperties, int>,
        IConnectionPropertiesRepository
    {
        public ConnectionPropertiesRepository(IMongoDatabase database, string collectionName)
            : base(database, collectionName) { }

        public ConnectionPropertiesRepository(IMongoDatabase database) : base(database) { }


        public async Task<List<ConnectionProperties>> Get(params int[] ids)
        {
            if(ids.Length == 0)
                return new List<ConnectionProperties>();
            return await GetCollection()
                .Find(Builders<ConnectionProperties>.Filter.In(x => x.Id, ids))
                .ToListAsync();
        }

        public Task<List<ConnectionProperties>> GetShortInfoList()
        {
            return GetCollection().Find(FilterDefinition<ConnectionProperties>.Empty)
                .Project(Builders<ConnectionProperties>.Projection
                    .Expression(i => new ConnectionProperties {Id = i.Id, Name = i.Name, Server = i.Server}))
                .ToListAsync();
        }

        public override async Task<ConnectionProperties> Insert(ConnectionProperties entity)
        {
            entity.Id = await GetNextId();
            return await base.Insert(entity);
        }
    }
}