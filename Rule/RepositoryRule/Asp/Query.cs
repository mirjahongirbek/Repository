using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public class Query
    {
        public string name { get; set; }
        public bool WithOffset { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
