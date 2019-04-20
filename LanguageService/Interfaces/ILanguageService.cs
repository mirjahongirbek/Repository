

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LanguageService.Interfaces
{
    public interface ILanguageService<TKey>
        where TKey : struct
    {
        Task<List<T>> GetList<T>(int langId);
        Task<T> GetById<T>(int langId, int id)
         where T : class;
        Task<T> Search<T>(int langId,Expression<Func<T, IComparable>> getProp, object value)
         where T : class;
       
    }

}
