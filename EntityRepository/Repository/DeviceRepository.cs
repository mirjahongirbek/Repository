using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using RepositoryRule.Base;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;
namespace EntityRepository.Repository
{
    public class DeviceRepository<T> : IUserDeviceRepository<T, int>
        where T : class, IUserDevice<int>
    {
        DbSet<T> _db;
        DbContext _context;
        ICacheRepository<T> _cache;
        public DeviceRepository(IDataContext context)
        {
            _db = context.DataContext.Set<T>();
            _context = context.DataContext;
        }
        public DeviceRepository(IDataContext context, ICacheRepository<T> cache) : this(context)
        {
            _cache = cache;
        }
        public virtual async Task Add(T model)
        {
            try
            {
                _cache.Add(model.Id.ToString(), model);
                _db.Add(model);
                Save();
            }
            catch (Exception ext)
            {
                throw new System.Exception("Add Exeption", ext);
            }

        }
        protected virtual void Save()
        {
            _context.SaveChangesAsync();
        }
        public virtual async Task Delete(int id)
        {
            try
            {
                var model = _cache.Find(id.ToString());
                if (model == null)
                {
                    model = await Get(id);
                }
                if (model != null)
                    _db.Remove(model);
                Save();
            }
            catch (Exception ext)
            {
                throw new Exception("", ext);
            }
        }
        public virtual async Task<bool> DeviceExist(T model, IAuthUser<int> user)
        {
            try
            {
                var device = await _db.FirstOrDefaultAsync(m => m.DeviceId == model.DeviceId && m.UserId == user.Id);
                if (device != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ext)
            {
                throw new Exception("", ext);
            }
        }
        public virtual async Task<T> Get(int id)
        {
            try
            {
                return await _db.FirstOrDefaultAsync(m => m.Id == id);

            }
            catch (Exception ext)
            {
                throw new Exception("", ext);
            }
        }
        public virtual async Task<IEnumerable<T>> FindAll()
        {
            try
            {
                return await _db.ToListAsync();

            }
            catch (Exception ext)
            {
                throw new Exception("", ext);
            }
        }
        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> selector)
        {
            try
            {
                return _db.Where(selector);

            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
        //TODO change
        public virtual async Task<AuthResult> LoginAsync(T model, IAuthUser<int> user)
        {
            try
            {
                var claims = GetIdentity(user.UserName, user.Roles.ToList());
                var authResult = State.State.GetAuth(claims, model);

                return authResult;
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
        public virtual async Task<T> GetByRefresh(string refreshToken)
        {
            try
            {
                var model = _cache.FindFirst(m => m.RefreshToken == refreshToken);
                if (model != null)
                {
                    return model;
                }
                return _db.FirstOrDefault(m => m.RefreshToken == refreshToken);
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
        public virtual async Task<AuthResult> UpdateToken(T model, IAuthUser<int> user)
        {
            try
            {
                if (model == null || model.Id == 0)
                {
                    throw new System.DivideByZeroException();
                }
                var claims = GetIdentity(user.UserName, user.Roles.ToList());
                var authResult = State.State.GetAuth(claims, model);
                model.AccessToken = authResult.AcessToken;
                model.RefreshToken = authResult.RefreshToken;
                await Update(model);
                return authResult;
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
        public virtual async Task<bool> Logout(string token)
        {
            try
            {
                var model = _cache.FindFirst(m => m.RefreshToken == token);
                if (model == null)
                {
                    model = _db.FirstOrDefault(m => m.AccessToken == token);
                    model.AccessToken = "";
                    model.RefreshToken = "";
                    await Update(model);
                }
                return true;
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }

        }
        public virtual async Task<bool> Logout(string token, string refresh)
        {
            return await Logout(token);
        }
        public virtual async Task Update(T model)
        {
            try
            {

                Save();
            }
            catch (Exception ext)
            {
                throw new Exception("", ext);
            }
        }
        //protected virtual void 
        private ClaimsIdentity GetIdentity(string username, List<IRoleUser<int>> rols)
        {

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                };
            foreach (var i in rols)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, i.Name));
            }

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }
              
    }
}