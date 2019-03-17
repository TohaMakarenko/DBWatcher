using MongoDB.Bson;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public class Sequence
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}