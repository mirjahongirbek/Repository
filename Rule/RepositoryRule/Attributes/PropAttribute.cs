using RepositoryRule.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PropAttribute : System.Attribute
    {
        public string Jwt { get; set; }
        public bool IsJwt { get; set; }
        public bool NotUpdate { get; set; }
        public object DefaultValue { get; set; } 
        public Type RunClass { get; set; }
        public PropAttribute()
        {
            Jwt = null;
            IsJwt = false;
            IsUpdate = false;
            DefaultValue = null;

        }
    }
    
    

}
