
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using RepositoryRule.Base;

namespace EntityRepository.Context
{
    public interface IDataContext
    {
        DbContext DataContext { get; }
    }
    public interface IEntityFunction : IDataFunction
    {

    }
    public class EntityFunction : IEntityFunction
    {
        DbContext _context;
        public EntityFunction(IDataContext db)
        {
            
            _context = db.DataContext;
        }
        public IEnumerable<T> CallProcedure<T>(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            return null;
        }

        public  object CallProcedure(string str)
        {
              var ss= _context.Database.ExecuteSqlCommand(str);
            return null;
        }

        public T CalProcedure<T>(string functionName, params object[] items)
        {
            throw new System.NotImplementedException();
        }
    }
}
