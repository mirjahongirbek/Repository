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
        string Password { get; set; }        
        ICollection<TRole> Roles { get; set; }
        IEnumerable<TDevice> DeviceList { get; set; }
    }
    public class MyProp<T>
    {
        private T _value;

        public T Value
        {
            get
            {
                // insert desired logic here
                return _value;
            }
            set
            {
                // insert desired logic here
                _value = value;
            }
        }

        public static implicit operator T(MyProp<T> value)
        {
            return value.Value;
        }

        public static implicit operator MyProp<T>(T value)
        {
            return new MyProp<T> { Value = value };
        }
    }
}
