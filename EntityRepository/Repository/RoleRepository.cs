using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EntityRepository.Repository
{
    public class RoleRepository<T> : IRoleRepository<T, int>
        where T : class, IRoleUser<int>
    {
        protected DbSet<T> _db;
        DbContext _context;
        public RoleRepository(IDataContext context)
        {
            _context = context.DataContext;
            _db = context.DataContext.Set<T>();
        }
        public void Add(T model)
        {
            try
            {
                _db.Add(model);
            }
            catch (Exception ext)
            {
                throw;
            }
            
        }

        public void Delete(T model)
        {
            try
            {
                _db.Remove(model);
            }
            catch(Exception ext)
            {
                throw;
            }
            
        }

        public void Delete(int id)
        {
            try
            {
                var role = _db.Find(id);
                if (role == null) return;
                _db.Remove(role);
            }
            catch(Exception ext)
            {
                throw;
            }
           
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
           return _db.Where(expression);
            
        }

        public T Get(int id)
        {
            return _db.Find(id);
        }

        public T GetFirst(Expression<Func<T, bool>> expression)
        {
           return _db.FirstOrDefault(expression);
        }

        public void Update(T model)
        {
            try
            {
                _db.Update(model);
            }
            catch(Exception ext)
            {
                throw;
            }
            
        }
    }
}
