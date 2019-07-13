using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using RepositoryRule.Attributes;

namespace ValidRepository
{
    public static class ValidState
    {
        public static List<string> CheckJwt(this object obj, ClaimsPrincipal user)
        {
            var type = obj.GetType();
            try
            {
                var errorList = new List<string>();
                foreach (var field in type.GetProperties())
                {
                    var attribute = field.GetCustomAttribute<PropsAttribute>();
                    if (attribute == null) attribute = new PropsAttribute(field.Name);

                    #region SetProperty

                    if (!string.IsNullOrEmpty(attribute.JWTKey))
                    {
                        var item = user.FindFirst(attribute.JWTKey).Value;
                        if (!string.IsNullOrEmpty(item))
                            field.SetValue(obj, Convert.ChangeType(item, field.PropertyType));
                    }

                    #endregion

                    var isRequired = field.GetCustomAttribute<RequiredAttribute>();

                    if (isRequired != null || attribute.Required)
                    {
                        var value = field.GetValue(type);
                        if (value == null) errorList.Add(attribute.Label ?? field.Name + "is null");
                    }
                }

                return errorList;
            }
            catch (Exception ext)
            {
                throw new Exception(ext.Message, ext);
            }
        }
    }
}