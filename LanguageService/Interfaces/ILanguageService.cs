

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LanguageService.Interfaces
{
    public interface ILanguageService<TKey>
        where TKey:struct
    {
        Task<List<T>> GetList<T>(int langId);
       
        Task<T> GetById<T>(int langId, int id)
        where T : class;
        Task<IEnumerable<T>> Find<T>(int langId, Expression<Func<T, bool>> expression)
            where T : class;
    }

}
