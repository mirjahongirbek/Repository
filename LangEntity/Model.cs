using Nest;
using System;
using System.Collections.Generic;

namespace LangEntity
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public class Model
    {
        public Guid Id { get; set; }
        public string Index { get; set; }
        public string EntityModel { get; set; }
        public string ProjectName { get; set; }
        public Dictionary<string, string> Langs = new Dictionary<string, string>();
        public List<Field> GetFields { get; set; }
        
    }


    public class Field
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
       
    }
}
