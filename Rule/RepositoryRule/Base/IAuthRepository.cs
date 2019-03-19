using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IAuthRepository<T, TRole, TDevice,TKey>
        where T : IAuthUser<TKey, TRole,TDevice>
        where TRole:class, IRoleUser<TKey>
        where TDevice:class, IUserDevice<TKey>

    {
        Task<T> GetAsync(T model);
        //  Task<AuthResult> LoginAsync(T model);
        Task<T> GetLoginOrEmail(string model);
        Task<T> GetUser(T model);
        Task<bool> IsLoginedAsync(T model);
        Task<bool> RegisterAsync(T model);
        Task Logout();
        Task Delete(TKey id);

    }

}
