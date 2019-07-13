using System;
using System.Collections.Generic;

namespace RepositoryRule.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        public EntityAttribute()
        {
            CheckRole = false;
            ReturnModel = true;
            NoTranslate = false;
        }

        public string Name { get; set; }
        public bool GetAll { get; set; }
        public bool IAuth { get; set; }
        public bool CheckRole { get; set; }
        public bool ReturnModel { get; set; }
        public bool NoTranslate { get; set; }
        public List<string> Fields { get; set; }
    }
}