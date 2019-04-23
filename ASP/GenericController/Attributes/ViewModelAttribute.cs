using RepositoryRule.Enums;
using System;


namespace GenericController.Attributes
{

public  class ViewModelAttribute:Attribute
    {
        public string Name { get;  }
        public Actions[] Actions { get; }
        public ViewModelAttribute(string name, params Actions[] actions)
        {
            this.Name = name;
            Actions = actions;
        }

    }
}
