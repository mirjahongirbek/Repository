using Microsoft.IdentityModel.Tokens;
using RepositoryRule.Entity;
using System.Text;

namespace EntityRepository.State
{
   public static class State
    {
        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOption.Key));
        }
    }
}
