using System;
using System.Collections.Generic;

namespace LangEntity
{
    public class FrontResult
    {
        public Guid Id { get; set; }
        public string Lang { get; set; }
        public string ProjectId { get; set; }
        public Dictionary<string, string> Result { get; set; }
    }

    public class FrontProject
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public Dictionary<string, string> Langs { get; set; }
        
    }
    
}
