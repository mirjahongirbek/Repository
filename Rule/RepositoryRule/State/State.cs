using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
namespace RepositoryRule.State
{
    public static class State
    {
        public static bool IsDevelopment { get; set; }
        public static ClaimsIdentity CreateClaim<TKey, TRole, T>(this IAuthUser<TKey, TRole, T> user)
            where TRole : class, IRoleUser<TKey>
            where T : class, IUserDevice<TKey>
        {
            var userTip = user.GetType();
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                };
            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));

            }
            foreach (var field in userTip.GetProperties())
            {
                field.GetCustomAttribute<AuthAttribute>();
            }
            return null;
        }
        public static List<string> CheckJwt(this object obj, ClaimsPrincipal user)
        {
            Type type = obj.GetType();
            try
            {
                List<string> errorList = new List<string>();
                foreach (var field in type.GetProperties())
                {
                    var attribute = field.GetCustomAttribute<PropsAttribute>();
                    if (attribute == null)
                    {
                        attribute = new PropsAttribute() { Name = field.Name };
                    }
                    #region SetProperty
                    var typeName = field.PropertyType.Name;
                    var item = user.FindFirst(attribute.JWTKey).Value.ConvertMe(typeName);
                    field.SetValue(obj, item);
                    #endregion
                    var isRequired = field.GetCustomAttribute<RequiredAttribute>();
                    if (isRequired != null)
                    {
                        var value = field.GetValue(type);
                        if (value == null)
                        {
                            errorList.Add(attribute.Label ?? field.Name + "is null");
                        }
                    }
                }
                return errorList;
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
        public static object ReturnObj(this object obj)
        {
            var type = obj.GetType();
            try
            {

                var attr = type.GetCustomAttribute<JohaAttribute>();
                if (attr == null)
                {
                    return obj;
                }
                if (attr.ReturnModel)
                {
                    return obj;
                }
                if (attr.Fields.Count == 0)
                {
                    return obj;
                }
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var i in attr.Fields)
                {
                    var prop = type.GetProperty(i);
                    var attribute = type.GetCustomAttribute<PropsAttribute>();
                    if (attribute == null)
                    {
                        attribute = new PropsAttribute() { Name = type.Name };
                    }
                    if (string.IsNullOrEmpty(attribute.Name))
                    {
                        attribute.Name = type.GetProperty(i).Name;
                    }
                    if (prop == null) continue;
                    result.Add(attribute.Name, prop.GetValue(obj));
                }
                return result;
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
        private static object ConvertMe(this string value, string type)
        {
            return null;
        }
    }
}
