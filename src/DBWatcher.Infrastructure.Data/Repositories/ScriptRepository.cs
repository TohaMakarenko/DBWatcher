using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class ScriptRepository : BaseEventRepository<Script, int>, IScriptRepository
    {
        public ScriptRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }
        public ScriptRepository(IMongoDatabase database) : base(database) { }

        public Task<List<Script>> GetShortInfoPage(int offset, int count)
        {
            return GetCollection().Find(FilterDefinition<Script>.Empty)
                .Skip(offset)
                .Limit(count)
                .Project(Builders<Script>.Projection.Expression(i => new Script
                    {Id = i.Id, Name = i.Name, Description = i.Description, Author = i.Author}))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Script>> Get()
        {
            return await GetCollection()
                .Find(x => true)
                .Project(x => new Script {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Author = x.Author
                })
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

        public override async Task<Script> Insert(Script entity)
        {
            entity.Id = await GetNextId();
            return await base.Insert(entity);
        }
    }
}