using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LangEntity.Project
{
    public class Projects
    {
        public Guid Id { get; set;  }
        public string Name { get; set; }
        public List<Entity> Entitys = new List<Entity>();
        [BsonIgnoreIfDefault]
        public Dictionary<string,int> Langs = new Dictionary<string, int>();
    }


}
