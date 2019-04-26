﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryRule.Entity;
using RepositoryRule.Exceptions;
using System;
using System.Diagnostics;
using System.IO;
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
                return new ResponseData()
                {
                    error = err
                };
            }


            if (result is ValidationExeption)
            {

            }

           // cBase.HttpContext.Response.StatusCode = status;
            return new ResponseData() { result = result };
        }
        
       
        public static ResponseData GetResponse(this ControllerBase cBase, object result, object err)
        {
            if (err != null)
            {

                cBase.Response.StatusCode = 400;
                return new ResponseData()
                {
                    error = err
                };
            }


            if (result is ValidationExeption)
            {

            }

            // cBase.HttpContext.Response.StatusCode = status;
            return new ResponseData() { result = result };
        }
        public static ResponseData GetResponse(this ControllerBase cBase, Responses responses)
        {
            switch (responses)
            {
                case Responses.Ok: { } break;
            }
        }
        public static readonly ResponseData Ok = new ResponseData { statusCode = 200, result = "Ok" };

        public static readonly ResponseData Created = new ResponseData { statusCode = 201, result = "Created" };


        public static readonly ResponseData BadRequest = new ResponseData { statusCode = 400, error = "Bad request" };

        public static readonly ResponseData Unauthorized = new ResponseData { statusCode = 401, error = "Unauthorized" }; //неавторизован

        public static readonly ResponseData Forbidden = new ResponseData { statusCode = 403, error = "Forbidden" }; //запрещено

        public static readonly ResponseData NotFound = new ResponseData { statusCode = 404, error = "Not found" };

        public static readonly ResponseData Conflict = new ResponseData { statusCode = 409, error = "Already exists" };

        public static readonly ResponseData InvalidParameters = new ResponseData { statusCode = 444, error = "Invalid parameters" };

        public static readonly ResponseData InternalServerError = new ResponseData { statusCode = 500, error = "Internal Server Error" };
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
        public static void Addwwroot(string filepath, IFormFile file)
        {

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\" + filepath);
            var bytes = new byte[file.Length];
            file.OpenReadStream().Read(bytes, 0, bytes.Length);
            File.WriteAllBytes(path, bytes);
        }
    }
    
}
