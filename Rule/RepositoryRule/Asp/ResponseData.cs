using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public class ResponseData
    {
        public ResponseData()
        {

        }
        public ResponseData(Responses responses) {
            Responses = responses;
        }
        public object result { get; set; }
        public object error { get; set; }
        public int statusCode { get; set; }
        public Responses Responses { get; } 
    }

}
