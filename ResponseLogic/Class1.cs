using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryRule.Entity;
using RepositoryRule.Exceptions;
using System;
using System.Diagnostics;
using System.Linq;

namespace SiteResponse
{
    public static class State
    {
        public static bool HasMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }
        public static int GetLang(this ControllerBase control)
        {
            try
            {
                var result = (string)control.Request.Headers.FirstOrDefault(m => m.Key == "Accept-Language").Value;
                if (string.IsNullOrEmpty(result)) return 0;
                return Convert.ToInt32(result);
            }
            catch (Exception ext) { }
            return 0;
        }

        public static ResponseData GetResponse(this ControllerBase cBase,
            object result = null,
            int status = 200,
            object err = null)
        {
            if (err != null)
            {

                cBase.Response.StatusCode = status;
                return new ResponseData
                {
                    error = new
                    {
                        err
                    }
                };
            }


            if (result is ValidationExeption)
            {

            }

            cBase.HttpContext.Response.StatusCode = status;
            return new ResponseData() { result = result };
        }
        public static object SerializeMe(this string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type,
                           new JsonSerializerSettings
                           {
                               NullValueHandling = NullValueHandling.Ignore
                           });
        }
        public static ResponseData ExceptionResult(this ControllerBase control,
            Exception ext,
            Stopwatch stop,
            object model = null)
        {
            stop.Stop();
            if (RepositoryRule.State.State.IsDevelopment)
            {
                return control.GetResponse(ext.Message);
            }
            string code = Guid.NewGuid().ToString();

            return control.GetResponse();

        }
    }
}
