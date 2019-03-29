using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public class ResponseData
    {
        public object result { get; set; }
        public object error { get; set; }
        public int status { get; set; }
    }
}
