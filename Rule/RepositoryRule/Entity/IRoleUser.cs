using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IRoleUser<TKey> : IEntity<TKey>
    {
        string Name { get; set; }
        int Position { get; set; }
        string Description { get; set; }
        //ICollection IAuthUser<TKey> Users 
    }
}
