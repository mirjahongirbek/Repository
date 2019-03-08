using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericController.Entity
{
    public class ResponseData
    {
        public dynamic result { get; set; }
        public dynamic error { get; set; }
        public int status { get; set; }
    }
    public class Request
    {
        public string name { get; set; }
        public string data { get; set; }
    }
    
}
