using RepositoryRule.Attributes;
using RepositoryRule.Entity;

namespace Entity
{
    [EntityDescription("selectData")]
    public class SelectData:IEntity<string>
    {
        [EntityDescription("SomeValue",FontType.Value)]
        public string Id { get; set; }
        [EntityDescription("Label", FontType.Label)]
        public string Name { get; set; }
    }
}
