using MongoDB.Bson.Serialization.Attributes;
using RepositoryRule.Attributes;
using RepositoryRule.Entity;

namespace Entity
{
   [EntityDescription("data")]
    public class Data : IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class EntityData : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
