using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IUserDeviceRepository<T, TKey>
        where T : IUserDevice<TKey>
    {
        Task Add(T model);
        Task<bool> DeviceExist(T model, IAuthUser<TKey> user);
        Task Update(T model);
        Task Delete(TKey id);
        Task<AuthResult> LoginAsync(T model, IAuthUser<TKey> user);
        Task<T> Get(TKey id);
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> selector);
        Task<AuthResult> UpdateToken(string refreshToken);

    }

}
