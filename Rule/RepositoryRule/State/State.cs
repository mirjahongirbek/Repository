using RepositoryRule.Attributes;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Collections;
using System.Linq;

namespace RepositoryRule.State
{
    public static class State
    {
        public static bool IsDevelopment { get; set; }
        public static bool NoHashPassword { get; set; }
        public static string RandomString(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[count];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public static T ConvertDictionary<T>(this Dictionary<string, object> model)
        {
           //var tip= typeof(T);
           var obj= (T)Activator.CreateInstance(typeof(T));
            var str = obj.GetType();
            foreach (var i in model)
            {
               var props= str.GetProperties().FirstOrDefault(m => m.Name.ToLower() == i.Key.ToLower());
                if (props != null)
                {
                    props.SetValue(obj, i.Value);
                }
            }
            return obj;
        }
        public static ClaimsIdentity CreateClaim<TKey, TRole, T>(this IAuthUser<TKey, TRole, T> user)
            where TRole : class, IRoleUser<TKey>
            where T : class, IUserDevice<TKey>
        {
            var userTip = user.GetType();
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));

            }
            
            foreach (var field in userTip.GetProperties())
            {

                var attr = field.GetCustomAttribute<AuthAttribute>();
                if(field.Name=="Id" && attr == null)
                {
                    claims.Add(new Claim(field.Name, user.Id.ToString()));
                }
                if (attr == null) continue;
                var value = field.GetValue(user).ToString();
                claims.Add(new Claim(attr.Name ?? field.Name, value));
            }
            if (claims.Count == 0)
            {
                return null;
            }
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
            
        }
        #region
        public  static string ComputeSha256Hash(this string rawData)
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
        public static  PropsAttribute GetProps(this System.Reflection.PropertyInfo i)
        {
            var attribute = i.GetCustomAttribute<PropsAttribute>();
            if (attribute == null && i.GetGetMethod().IsVirtual)
            {
                return null;
            }
            var foregnKey = i.GetCustomAttribute<ForeignKeyAttribute>();
            var stringLength = i.GetCustomAttribute<StringLengthAttribute>();
            var require = i.GetCustomAttribute<RequiredAttribute>();
            PropsAttribute attr;
            if (attribute == null)
            {
                attr = new PropsAttribute(i.Name);
            }
            else
            {
                attr = attribute;
            }
           
            if (foregnKey != null)
            {
                attr.ForeignTable = attr.ForeignTable ?? foregnKey.Name;
            }
            if (stringLength != null)
            {
                attr.MaxLength = stringLength.MaximumLength;
                attr.MinLength = stringLength.MinimumLength;
            }
            if (require != null)
            {
                attr.Required = true;
            }
            return attr;
        }
        //TODO change JWT
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
                        attribute = new PropsAttribute(name:field.Name);
                    }
                    #region SetProperty
                    if (!string.IsNullOrEmpty(attribute.JWTKey))
                    {
                        var item = user.FindFirst(attribute.JWTKey).Value;
                        if (!string.IsNullOrEmpty(item))
                        {
                            field.SetValue(obj, Convert.ChangeType(item, field.PropertyType));
                        }
                    }
                    #endregion
                    var isRequired = field.GetCustomAttribute<RequiredAttribute>();
                    if (isRequired != null || attribute.Required)
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
        public static object ListDataParse(object data, Type type)
        {
            if(data is IEnumerable)
            {

            }
            return data;
        }
        #endregion

    }
}
