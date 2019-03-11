using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    public class NoVisibleAttribute : System.Attribute
    {
        public bool IsVisble { get; set; }
        public NoVisibleAttribute()
        {
            IsVisble = false;
        }
        public NoVisibleAttribute(bool isVisible)
        {
            IsVisble = isVisible;
        }
    }

}
