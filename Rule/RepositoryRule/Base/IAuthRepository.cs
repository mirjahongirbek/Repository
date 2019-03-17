using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IAuthRepository<T, TKey>
        where T : IAuthEntiy<TKey>
    {
        T Get(string userName);
        T Get(TKey id);

        void Update(T model);
        void Delete(TKey id);
    }
}
