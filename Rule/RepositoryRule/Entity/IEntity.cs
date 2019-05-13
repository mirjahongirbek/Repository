using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
