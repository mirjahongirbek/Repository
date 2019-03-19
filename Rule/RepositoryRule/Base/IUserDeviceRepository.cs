using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IUserDeviceRepository<T,TRole, TKey>
        where T : class, IUserDevice<TKey>
        where TRole:class, IRoleUser<TKey>
    {
        Task Add(T model);
        Task<bool> DeviceExist(T model, IAuthUser<TKey,TRole ,T> user);
        Task Update(T model);
        Task Delete(TKey id);
        Task<AuthResult> LoginAsync(T model, IAuthUser<TKey, TRole, T> user);
        Task<T> Get(TKey id);
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> selector);
        Task<AuthResult> UpdateToken(T model, IAuthUser<TKey, TRole, T> user);
        Task<T> GetByRefresh(string refreshToken);
        Task<bool> Logout(string token);
    }

}
