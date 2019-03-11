using MongoDB.Bson.Serialization.Attributes;
using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using System.Collections.Generic;

namespace Entity
{
    [EntityDescription("data")]
    public class Data : IEntity<string>
    {
        [EntityDescription("Edit", FontType.EditDelete, ShowAdd = false, Label ="Hello world")]
        [BsonId]
        public string Id { get; set; }
        [EntityDescription("Name", FontType.String)]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        [EntityDescription("Description Select", FontType.Select, "selectData")]
        public string Description { get; set; }

        [EntityDescription("Hello world",FontType.StringList)]
        public List<string> Lists { get; set; }
    }
}
