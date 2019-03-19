using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JohaAttribute : System.Attribute
    {
        
        public string Name { get; set; }
        public bool GetAll { get; set; }
        public bool IAuth { get; set; }
        public bool CheckRole { get; set; }

    }


}
