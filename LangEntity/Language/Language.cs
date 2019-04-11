

using MongoDB.Bson.Serialization.Attributes;
using RepositoryRule.Entity;

namespace LangEntity.Language
{
     public class Language:IEntity<string>
     {
        [BsonId]
        public string Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
     }
}
