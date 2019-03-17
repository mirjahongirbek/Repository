using MongoDB.Bson.Serialization.Attributes;
using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using System.Collections.Generic;

namespace Entity
{
    [EntityDescription("data")]
    public class Data : IEntity<string>
    {
        [EntityDescription(DefaultLabel:"Edit", font: FontType.EditDelete, hideAdd:false, hideshow:true)]
        [BsonId]
        public string Id { get; set; }
        [EntityDescription("Name", FontType.String)]
        public string Name { get; set; }
        public string sdds { get; set; }
        [BsonIgnoreIfNull]
        [EntityDescription("Description Select", FontType.Select)]
        public string Description { get; set; }
        [EntityDescription("Hello world",FontType.StringList)]
        public List<string> Lists { get; set; }
    }
    
}
