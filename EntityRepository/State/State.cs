using Microsoft.IdentityModel.Tokens;
using RepositoryRule.Entity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EntityRepository.State
{
   public  class State
    {
        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOption.Key));
        }
        public static AuthResult GetAuth<TKey>( System.Security.Claims.ClaimsIdentity claim, IUserDevice<TKey> model)
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

            AuthResult result = new AuthResult()
            {
                AccessToken = encodedJwt,
                RefreshToken = ComputeSha256Hash(now.ToLongDateString() + now.ToLongTimeString() + RepositoryRule.State.State.RandomString(6)),
                AccessTime = now,
            };
            return result;
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
