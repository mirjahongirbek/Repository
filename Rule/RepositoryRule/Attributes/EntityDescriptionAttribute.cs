using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    public class EntityDescriptionAttribute : System.Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EntityDescriptionAttribute(string name)
        {
            Name = name;
        }
        public EntityDescriptionAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
    public class NoVisible : System.Attribute
    {
        public bool IsVisble { get; set; }
        public NoVisible()
        {
            IsVisble = false;
        }
        public NoVisible(bool isVisible)
        {
            IsVisble = isVisible;
        }
    }
}
