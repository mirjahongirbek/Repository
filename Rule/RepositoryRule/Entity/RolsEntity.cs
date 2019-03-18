using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IRolsEntity<TKey> : IEntity<TKey>
    {
        string RoleName { get; set; }
        string Description { get; set; }

    }
    public interface  ICommandResult
    {
        bool Success { get; }
        string ErrorText { get; set; }
    }
}
