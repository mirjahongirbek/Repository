using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntityRepository.Repository
{
    public class AuthRepository<T> : IAuthRepository<T, int>
        where T:class, IAuthUser<int>
    {
        protected DbSet<T> _db;
        public AuthRepository(IDataContext context)
        {
            _db = context.DataContext.Set<T>();
        }
        public virtual async  Task Delete(int id)
        {
          
        }

        public virtual Task<AuthResult> LoginAsync(T model)
        {
            //model.UserName
            bool isEmail = Regex.IsMatch(model.UserName, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
           var password= HashPassword(model.Password);
            if (isEmail)
            {
                _db.FirstOrDefault();
            }
             _db.FirstOrDefault();
            return null;
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
        public virtual Task Logout()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<AuthResult> RegisterAsync()
        {
            throw new System.NotImplementedException();
        }
        public async Task<T> Get(T model)
        {
            return _db.FirstOrDefault(m => model.UserName == m.UserName);
        }
        protected virtual string HashPassword(string password)
        {
            return password;
        }

        public Task<T> GetAsync(T model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> GetUser(T model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                {
                    return null;
                }
                var password = HashPassword(model.Password);
                return _db.FirstOrDefault(m => m.UserName == model.UserName && m.Password == password);

            }catch(Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
            
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
