using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LiteDB;
using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace LiteRepository.Repository
{
    //public class LiteRepository<T> : IRepositoryBase<T, string>
    //    where T : class, IEntity<string>
    //{
    //    private LiteCollection<T> _db;
    //    public LiteRepository(IDataContext context)
    //    {
    //        var name = nameof(T);
    //        _db = context.Database.GetCollection<T>(name);
    //    }
    //    #region Add
    //    public void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
    //            if(string.IsNullOrEmpty(model.Id))
    //            {
    //                model.Id = ObjectId.NewObjectId().ToString();
    //            }
    //            _db.Insert(model);
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task AddAsync(T module, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
    //            Add(module);
    //            return Task.CompletedTask;
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
    //            foreach(var i in models)
    //            {
    //                if (string.IsNullOrEmpty(i.Id))
    //                {
    //                    i.Id = ObjectId.NewObjectId().ToString();
    //                }
    //            }
    //            _db.InsertBulk(models);
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
    //            AddRange(models);

    //            return Task.CompletedTask;
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }
    //    #region Procedure
    //    public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task<IEnumerable<T>> CallProcedure(string str)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public T CalProcedure(string functinname, object[] item, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }
    //    #endregion
    //    public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
                
    //           return _db.LongCount();
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
    //           return _db.LongCount(expression);
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
    //            var props = typeof(T).GetProperty(field);
    //            int result = _db.Count(m => props.GetValue(m, null) == value);
    //            return result;
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {
    //            _db.Delete(model.Id);
    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public T Delete(TKey id)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public T Delete(string id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task DeleteManyAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public IEnumerable<T> FindAll()
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task<IEnumerable<T>> FindAllAsync()
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception ext)
    //        {
    //            throw ext;
    //        }
    //    }

    //    public IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try {

    //        } catch (Exception ext) { throw ext; }
    //    }

    //    public IEnumerable<T> FindReverse(int offset, int limit)
    //    {
    //        try {
    //        } catch (Exception ext) { throw ext; }
    //    }

    //    public IEnumerable<T> FindReverse(string key, string value, int offset, int limit)
    //    {
    //        try {
    //        } catch (Exception ext) { throw ext; }
    //    }

    //    public Task<IEnumerable<T>> FindReverseAsync(int offset, int limit)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public Task<IEnumerable<T>> FindReverseAsync(string key, string value, int offset, int limit)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public T Get(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public T Get(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<T> GetAsync(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public Task<T> GetAsync(string id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public T GetFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public Type GetGenericType()
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }

    //    public Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
    //    {
    //        try { } catch (Exception ext) { throw ext; }
    //    }
    //}
}
