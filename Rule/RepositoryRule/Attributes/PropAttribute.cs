using System;

namespace RepositoryRule.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PropAttribute : Attribute
    {
        public PropAttribute()
        {
            Jwt = null;
            IsJwt = false;
            NotUpdate = false;
            DefaultValue = null;
        }

        public string Jwt { get; set; }
        public bool IsJwt { get; set; }
        public bool NotUpdate { get; set; }
        public object DefaultValue { get; set; }
        public Type RunClass { get; set; }
    }
}