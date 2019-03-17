using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IAuthUser<TKey> : IEntity<TKey>
    {
        string UserName { get; set; }
        string Email { get; set; }
        string Passwor { get; set; }

        ICollection<IRoleUser<TKey>> Roles { get; set; }
        ICollection<IUserDevice<TKey>> DeviceList { get; set; }
    }
}
