using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryRule.Base;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;
using System;
using System.Linq;
using RepositoryRule.State;
using System.Threading.Tasks;

namespace EntityRepository.Repository
{
    public class AuthRepository<T, TRole, TDevice> : IAuthRepository<T, TRole, TDevice, int>
        where T:class, IAuthUser<int, TRole, TDevice>
        where TRole: class, IRoleUser<int>
        where TDevice:class, IUserDevice<int>
    {
        ICacheRepository<T> _cache;
        protected DbSet<T> _db;
        DbContext _context;
        public AuthRepository(IDataContext context)
        {
            _db = context.DataContext.Set<T>();
            _context = context.DataContext;
        }
        public AuthRepository(IDataContext context, ICacheRepository<T> cache) : this(context)
        {
            _cache = cache;
        }
        //TODO finish
        private T GetByCache(int id)
        {
          return  _cache?.Find(id.ToString());
        }
        public virtual async  Task Delete(int id)
        {
            try
            {
                var device=  GetByCache(id);
                if(device== null)
                 device = _db.Find(id);
                if(device!=null)
                _db.Remove(device);
            }catch(Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
          
        }
        public virtual async Task<bool> IsLoginedAsync(T model)
        {
            var result= _db.FirstOrDefault(m => m.UserName == model.UserName || m.Email == model.UserName);
            if(result== null)
            {
                return true;
            }
            return false;
        }
        //TODO NotImplement
        public virtual Task Logout(string token)
        {
            var access= token.Split(" ")[1];
            return null;
        }
      
        
        protected virtual string HashPassword(string password)
        {
            if (RepositoryRule.State.State.NoHashPassword)
                return password;
                return password.ComputeSha256Hash();

        }

      public virtual async Task<T> GetUser(T model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                {
                    return null;
                }
                var password = HashPassword(model.Password);
               return await _db.Include(m => m.Roles).Include(m=>m.DeviceList).FirstOrDefaultAsync(m => m.UserName == model.UserName && m.Password == password);
                

            }catch(Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
            
        }
        public virtual  async Task<T> GetLoginOrEmail(string model)
        {
           return _db.FirstOrDefault(m => m.UserName == model || m.Email == model);
        }
       
        public async Task<bool> RegisterAsync(T model)
        {
            model.Password = HashPassword(model.Password);
            await _db.AddAsync(model);
            _context.SaveChanges();
            return true;
        }

        public async Task<T> GetMe(string id)
        {
            try
            {
                var UserId = Convert.ToInt32(id);
                var user= await _db.Include(m=>m.Roles).FirstOrDefaultAsync(m=>m.Id==UserId);
                user.DeviceList = null;
                return user;
            }catch(Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
           
        }
    }
}
