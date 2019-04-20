using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public class PostResponse
    {
        public IEnumerable Items { get; set; }
        public long Count { get; set; }
    }
}
