using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    public class AuthAttribute : System.Attribute
    {
        public bool AddJWt { get; set; }
        public string Name { get; set; }
        public AuthAttribute()
        {
            AddJWt = true;
        }
        
    }

}
