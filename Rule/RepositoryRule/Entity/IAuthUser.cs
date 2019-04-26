using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IAuthUser<TKey,TRole,TDevice> : IEntity<TKey>
        where TRole:class, IRoleUser<TKey>
        where TDevice:class, IUserDevice<TKey>
        
    {
        string UserName { get; set; }
        string Email { get; set; }
        string Salt { get; set; }
        string Password { get; set; }        
        ICollection<TRole> Roles { get; set; }
        IEnumerable<TDevice> DeviceList { get; set; }
    }
}
