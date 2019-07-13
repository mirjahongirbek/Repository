using System;

namespace RepositoryRule.Attributes
{
    public class AuthAttribute : Attribute
    {
        public AuthAttribute()
        {
            AddJWt = true;
        }

        public bool AddJWt { get; set; }
        public string Name { get; set; }
    }
}