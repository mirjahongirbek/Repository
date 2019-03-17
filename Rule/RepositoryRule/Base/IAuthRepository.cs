using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IAuthRepository<T, TKey>
        where T : IAuthUser<TKey>
    {
        Task<T> GetAsync(T model);
        Task<AuthResult> LoginAsync(T model);
        Task<bool> IsLoginedAsync(T model);
        Task<AuthResult> RegisterAsync();
        Task Logout();
        Task Delete(TKey id);

    }

}
