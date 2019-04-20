using System;
using System.Collections.Generic;

namespace LangEntity.Project
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Field> GetFields { get; set; }
    }


}
