using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class ConnectionPropertiesRepository : BaseRepository<ConnectionProperties>, IConnectionPropertiesRepository
    {
        public ConnectionPropertiesRepository(IMongoDatabase database, string collectionName)
            : base(database, collectionName) { }

        public ConnectionPropertiesRepository(IMongoDatabase database) : base(database, "ConnectionProperties") { }


        public Task<ConnectionProperties> GetById(Guid id)
        {
            return GetCollection().Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<ConnectionProperties>> GetShortInfoList()
        {
            return GetCollection().Find(FilterDefinition<ConnectionProperties>.Empty)
                .Project(Builders<ConnectionProperties>.Projection
                    .Expression(i => new ConnectionProperties() {Id = i.Id, Name = i.Name, Server = i.Server}))
                .ToListAsync();
        }

        public Task InsertConnection(ConnectionProperties connectionProperties)
        {
            return GetCollection().InsertOneAsync(connectionProperties);
        }

        public Task UpdateConnection(ConnectionProperties connectionProperties)
        {
            return GetCollection().ReplaceOneAsync(i => i.Id == connectionProperties.Id, connectionProperties,
                new UpdateOptions() {IsUpsert = true});
        }
    }
}