using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IAuthEntiy<TKey> : IEntity<TKey>
    {
        string UserName { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
       ICollection<IRoleUser<TKey>> Rols { get; set; }
    }
}
