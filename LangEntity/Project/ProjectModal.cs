
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;

namespace LangEntity.Project
{

    //Project Main entity Class
    public  class LangProject:IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Projects> Projects = new List<Projects>();
        public DateTime CreateTime { get; set; }   
        public DateTime LastUpdateTime { get; set; }
        public List<Projects> FrontProjects = new List<Projects>();

    }


}
