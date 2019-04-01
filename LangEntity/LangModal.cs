using Nest;
using System.Collections.Generic;

namespace LangEntity
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public class LangModal
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Field> GetFields { get; set; }
    }
}
