using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class ScriptRepository : BaseRepository<Script>, IScriptRepository
    {
        public ScriptRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }
        public ScriptRepository(IMongoDatabase database) : base(database, "Scripts") { }

        public Task<Script> GetById(Guid id)
        {
            return GetCollection().Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Script>> GetShortInfoPage(int offset, int count)
        {
            return GetCollection().Find(FilterDefinition<Script>.Empty)
                .Skip(offset)
                .Limit(count)
                .Project(Builders<Script>.Projection.Expression(i => new Script()
                    {Id = i.Id, Name = i.Name, Description = i.Description, Author = i.Author}))
                .ToListAsync();
        }

        public Task<List<Script>> FindByName(string name)
        {
            return GetCollection().Find(i => i.Name.Contains(name)).ToListAsync();
        }

        public Task<List<Script>> FindByNameOrDescription(string name, string description)
        {
            return GetCollection().Find(i => i.Name.Contains(name) || i.Description.Contains(description))
                .ToListAsync();
        }

        public Task InsertScript(Script script)
        {
            return GetCollection().InsertOneAsync(script);
        }

        public Task UpdateScript(Script script)
        {
            return GetCollection().ReplaceOneAsync(i => i.Id == script.Id, script,
                new UpdateOptions() {IsUpsert = true});
        }
    }
}