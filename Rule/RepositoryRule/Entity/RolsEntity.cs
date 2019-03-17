using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IRolsEntity<TKey> : IEntity<TKey>
    {
        string RoleName { get; set; }
        string Description { get; set; }

    }
    public abstract class AuthResult
    {

    }
}
