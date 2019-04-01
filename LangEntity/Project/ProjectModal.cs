
using Nest;
using System;
using System.Collections.Generic;

namespace LangEntity.Project
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public  class LangProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Projects> Projects { get; set; }
        public DateTime CreateTime { get; set; }   
        public DateTime LastUpdateTime { get; set; }
        public List<Projects> FrontProjects { get; set; }

    }

    public class Projects
    {
        public Guid Id { get; set;  }
        public string Name { get; set; }
        public Dictionary<string, string> Entitys { get; set; }

    }

        
}
