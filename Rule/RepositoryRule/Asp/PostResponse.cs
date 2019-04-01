using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public class PostResponse
    {
        public List<object> Items { get; set; }
        public long Count { get; set; }
    }
}
