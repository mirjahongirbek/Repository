using Microsoft.IdentityModel.Tokens;
using RepositoryRule.Entity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EntityRepository.State
{
   public  class State
    {
        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOption.Key));
        }
        public static AuthResult GetAuth<TKey>(System.Security.Claims.ClaimsIdentity claim, IUserDevice<TKey> model)
        {
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOption.ISSUER,
                    audience: AuthOption.AUDINECE,
                    notBefore: now,
                    claims: claim.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOption.LifeTime)),
                    signingCredentials: new SigningCredentials(State.GetSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            //model.RefreshToken = "123456789";// joha

            AuthResult result = new AuthResult()
            {
                AcessToken = encodedJwt,
                RefreshToken ="change",


            };
            return result;
        }
    }
}
