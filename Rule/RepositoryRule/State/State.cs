using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RepositoryRule.State
{
    public static class State
    {
        public static bool CheckJwt(this object obj, ClaimsPrincipal user, Type type= null)
        {
            if(type== null)
            {
                type = obj.GetType();
            }
            foreach(var field in type.GetProperties())
            {
                
            }

            return false;
        }
    }
}
