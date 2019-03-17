using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IRoleRepository<T, TKey>
        where T: IRolsEntity<TKey>
    {
        T Get(string username);
        T Get(TKey id);
        T Add(T model);
        void Update(T model);
        void Delete(TKey id);
        IEnumerable<IAuthEntiy<TKey>> GetUsers(TKey id);

    }
}
