using Marten;
using MartenRepository.Context;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MartenRepository.Repository
{
    public class MartenRepository<T, TKey> : IRepositoryBase<T, TKey>
            where T : class, IEntity<TKey>
    {
        private IDocumentStore _store;
        public MartenRepository(IMartenContext context)
        {
            _store = context.Store;


        }
        //TODO
        public void Add(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {

                    session.Store(model);
                    session.SaveChanges();
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }

        }

        public Task AddAsync(T module, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                Add(module);
                return Task.CompletedTask;
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public void AddRange(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {

                _store.BulkInsert(models, BulkInsertMode.IgnoreDuplicates);

                
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public Task AddRangeAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                AddRange(models);
                return Task.CompletedTask;
            }
            catch (Exception ext) { throw ext; }
        }
        #region Count
        public long Count([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {

                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().CountAsync().Result;
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public long Count(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().CountAsync(expression).Result;
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public long Count(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    var props = typeof(T).GetProperty(field);
                    var result = session.Query<T>().CountAsync(m => props.GetValue(m, null) == value).Result;
                    return result;

                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }
        #endregion

        #region Delete
        public void Delate(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    session.Delete<T>(model);

                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public Task DelateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    session.Delete<T>(model);
                    return Task.CompletedTask;
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public T Delete(TKey id)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                   var model= Get(id);
                    if(model== null) { return null; }
                    session.Delete(model);
                    return model;
                    //if (id is string stringId)
                    //{
                    //    session.Delete<T>(stringId);
                    //}
                    //else if (id is Guid guid)
                    //{
                    //    session.Delete<T>(guid);
                    //}
                    //else if (id is int intId)
                    //{
                    //    session.Delete<T>(intId);
                    //}
                    //else if (id is long longid)
                    //{
                    //    session.Delete<T>(longid);
                    //}

                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public void DeleteMany(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {

                    session.DeleteWhere<T>(expression);
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                DeleteMany(expression);
                return Task.CompletedTask;

            }
            catch (Exception ext)
            {
                throw ext;
            }
        }
        #endregion
        #region Find
        public IEnumerable<T> Find(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().Where(selector);

                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().Where(keySelector);

                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public IEnumerable<T> Find(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    var props = typeof(T).GetProperty(field);
                    var result = session.Query<T>().Where(m => props.GetValue(m, null) == value);
                    return result;
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public IEnumerable<T> Find(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {

                    var props = typeof(T).GetProperty(field);
                    var result = session.Query<T>().Where(m => props.GetValue(m, null) == value).Skip(offset).Take(limit);
                    return result;


                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public IEnumerable<T> FindAll()
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>();

                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return (session.Query<T>());
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> keySelector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().Where(keySelector);
                }
            }
            catch (Exception ext) { throw ext; }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> selector, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                  return  session.Query<T>().Where(selector).Skip(offset).Take(limit);
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(string field, string value, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                return Find(field, value);
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(string field, string value, int offset, int limit, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                return Find(field, value, offset, limit);
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public IEnumerable<T> FindReverse(Expression<Func<T, bool>> selector, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().Where(selector).OrderByDescending(m => m.Id);
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public IEnumerable<T> FindReverse(int offset, int limit)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                    return session.Query<T>().OrderByDescending(m => m.Id).Skip(offset).Take(limit);
                }

            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public IEnumerable<T> FindReverse(string key, string value, int offset, int limit)
        {
            try
            {
                using(var session= _store.OpenSession())
                {
                    var props = typeof(T).GetProperty(key);
                   return session.Query<T>().Where<T>(m => props.GetValue(m, null) == value).OrderByDescending(m=>m.Id).Skip(offset).Take(limit);
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<IEnumerable<T>> FindReverseAsync(int offset, int limit)
        {
            try
            {
                using (var session = _store.OpenSession())
                {
                   return session.Query<T>().OrderByDescending(m => m.Id).Skip(offset).Take(limit);
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<IEnumerable<T>> FindReverseAsync(string key, string value, int offset, int limit)
        {
            try
            {
                return FindReverse(key, value, offset, limit);
            }
            catch (Exception ext) { throw ext; }
        }
        #endregion
        #region Get

        public T Get(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using(var session = _store.OpenSession())
                {
                    if (id is string sid)return session.Load<T>(sid);
                    else if (id is Guid guid)return session.Load<T>(guid);
                    else if (id is int intId) return session.Load<T>(intId);
                    else if (id is long longId) return session.Load<T>(longId);
                    return null;
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<T> GetAsync(TKey id, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                return Get(id);

            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public T GetFirst(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using(var session= _store.OpenSession())
                {
                   return session.Query<T>().FirstOrDefault(expression);
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                return GetFirst(expression);
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public Type GetGenericType()
        {
            try
            {
                return typeof(T);
            }
            catch (Exception ext) { throw ext; }
        }
        #endregion
        #region Update
        public void Update(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task UpdateAsync(T model, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                
                using(var session = _store.OpenSession())
                {
                    session.Update<T>(model);    
                }
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }
       

        public void UpdateMany(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using(var session= _store.OpenSession())
                {

                    foreach (var i in models)
                    {
                        session.Update<T>(i);
                    }
                    session.SaveChanges();
                }
                
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public async Task UpdateManyAsync(List<T> models, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                using (var session = _store.OpenSession())
                {

                    foreach (var i in models)
                    {
                        session.Update<T>(i);
                    }
                   await session.SaveChangesAsync();
                }

            }
            catch (Exception ext)
            {
                throw ext;
            }
        }
        #endregion
        #region Procedure
        public IEnumerable<T> CallProcedure(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public Task<IEnumerable<T>> CallProcedure(string str)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public T CalProcedure(string functinname, object[] item, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }
        #endregion
    }

}
