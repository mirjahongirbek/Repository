using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IRoleRepository<T, TKey>
        where T : class, IRoleUser<TKey>
    {
        void Add(T model);
        void Delete(T model);
        void Delete(TKey id);
        void Update(T model);
        T Get(TKey id);
        T GetFirst(Expression<Func<T, bool>> expression);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}