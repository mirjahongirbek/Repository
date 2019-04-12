

using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageService.Interfaces
{
    public interface ILanguageService<TKey>
        where TKey:struct
    {
        Task<List<T>> GetList<T>(int langId);
        Task<T> Get<T>(int langId, Dictionary<string, object> list)
            where T : class;
        Task<T> GetById<T>(int langId, int id)
        where T : class;

    }

}
