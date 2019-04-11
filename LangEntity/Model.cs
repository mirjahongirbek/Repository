using LangEntity.Project;
using MongoDB.Bson.Serialization.Attributes;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;

namespace LangEntity
{
    public class TraficcModel
    {
        public Guid Id { get; set; }
        public string Index { get; set; }
        public string EntityModel { get; set; }
        public string ProjectName { get; set; }
        public Dictionary<string, string> Langs = new Dictionary<string, string>();
        public List<Field> GetFields { get; set; }

    }
    public class EntityData:IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        public int LangId { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public Guid Guid { get; set; }
        [BsonIgnore]
        public string Name { get; set; }


    }




}
