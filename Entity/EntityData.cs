using RepositoryRule.Entity;

namespace Entity
{
    public class EntityData : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
