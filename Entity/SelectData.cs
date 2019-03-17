using RepositoryRule.Attributes;
using RepositoryRule.Entity;

namespace Entity
{
    [Joha("selectData")]
    public class SelectData:IEntity<string>
    {
        [Props("SomeValue",FontType.Value)]
        public string Id { get; set; }
        [Props("Label", FontType.Label)]
        public string Name { get; set; }
    }
}
