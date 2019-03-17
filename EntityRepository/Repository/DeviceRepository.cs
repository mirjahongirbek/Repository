using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryRule.Base;
using RepositoryRule.CacheRepository;
using RepositoryRule.Entity;
using EntityRepository.State;
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

        public virtual async Task<IEnumerable<T>> GetAll()
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

            }catch(Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
        public virtual async Task<AuthResult> LoginAsync(T model, IAuthUser<int> user)
        {
            try
            {
               var claims= GetIdentity(user.UserName,user.Roles.ToList());
                var now = DateTime.Now;
                var jwt = new JwtSecurityToken(
                     issuer: AuthOption.ISSUER,
                     audience: AuthOption.AUDINECE,
                     notBefore: now,
                     claims:claims.Claims,
                     expires: now.Add(TimeSpan.FromMinutes(AuthOption.LifeTime)),
                     signingCredentials: new SigningCredentials(State.State.GetSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                model.RefreshToken = "123456789";// joha
                
                model.RefreshToken
               var result= new AuthResult
                {
                     AcessToken= encodedJwt,
                     RefreshToken="sd"
                };
                return result;
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }

        public virtual async Task<AuthResult> UpdateToken(string refreshToken)
        {
            try
            {
                var device= _cache.FindFirst(m => m.RefreshToken == refreshToken);
               if(device== null)
                {

                }

                Save();
            }
            catch (Exception ext)
            {
                throw new Exception("", ext);
            }
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

            /*
             var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
             
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
 
            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
  
             
             */
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                };
            foreach(var i in rols)
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
/*
  [HttpPost("/token")]
        public async Task Token()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];
 
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }
 
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
             
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
 
            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
 
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
 
            // если пользователя не найдено
            return null;
        }
     */
