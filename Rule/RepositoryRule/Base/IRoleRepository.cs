using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IRoleRepository<T, TKey>
         where T : IRoleUser<TKey>
    {
        void Add(T model);
        void Delete(T model);
        void Delete(TKey id);
        void Update(T model);
    }

}
