namespace RepositoryRule.Entity
{
    public interface IRolsEntity<TKey> : IEntity<TKey>
    {
        string RoleName { get; set; }
        string Description { get; set; }
    }
}