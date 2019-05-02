using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryRule.Entity;
using RepositoryRule.Exceptions;
using System;
using System.Collections.Generic;
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
        public static int GetLang(this ControllerBase control, int defaultValue=0)
        {
            try
            {
                var result = (string)control.Request.Headers.FirstOrDefault(m => m.Key == "Accept-Language").Value;
                if (string.IsNullOrEmpty(result)) return defaultValue;
                return Convert.ToInt32(result);
            }
            catch (Exception ext) { }
            return 0;
        }


        public static ResponseData GetResponse(this ControllerBase cBase,
            object result = null,
            int status = 200,
            object err = null )
        {
            if (err != null)
            {

                cBase.Response.StatusCode = status==200?400:status;
                return new ResponseData()
                {
                    error = err
                };
            }
            #region
            if (result is ValidExeption valid)
            {
                cBase.Response.StatusCode = valid.HttpStatusCode;
                Valid(valid);
            }
            #endregion
            if (result is Exception exception)
            {
                cBase.Response.StatusCode = 400;
                return new ResponseData()
                { error = new { message = exception.Message } };
            }
            if (result is ResponseData response)
            {
                response.statusCode = response.statusCode == 0 ? 200 : response.statusCode;
                cBase.Response.StatusCode = response.statusCode;
                return response;
            }
            if (result is Responses responses)
            {
                return GetResponse(cBase, responses);
            }
            return new ResponseData() { result = result };
        }
        private static ResponseData Valid(ValidExeption valid)
        {
            if(valid.Validators!= null)
            {
                return new ResponseData
                {
                    error = valid.Validators
                };
            }
            if(valid.Validator!= null)
            {
                return new ResponseData
                {
                     error= valid.Validator
                };
            } if(valid.Error!= null)
            {
                return new ResponseData()
                {
                    error = valid.Error
                }; 

            }
            return new ResponseData()
            {
                error=new
                {
                    message="Http Status Code " + valid.HttpStatusCode
                }
            };
            
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
            return GetResponse(cBase, result, 200, err);

        }
        public static ResponseData GetResponse(this ControllerBase cBase, Responses responses)
        {
           var result= ResponseList.FirstOrDefault(m => m.Key == responses);
            if(result.Value== null)
            {
                cBase.Response.StatusCode = 400;
                return new ResponseData() { error= new {  message="result not implament"} };
            }
            cBase.Response.StatusCode = result.Value.statusCode;
            return result.Value;
            
        }


        #region 
        private static Dictionary<Responses, ResponseData> _response;
        private static Dictionary<Responses, ResponseData> ResponseList
        {
            get
            {
                if (_response == null)
                {
                    _response = new Dictionary<Responses, ResponseData>()
                    {
                        {Responses.Ok, new  ResponseData { statusCode = 200, result = new {success=true} } },
                        {Responses.ModelIsNull, new ResponseData(){ statusCode= 201, result= new {success= true, created= true} } },
                        {Responses.Conflict, new ResponseData(){ statusCode=400, error= new { message= "conflict"} } },
                        {Responses.InvalidParameters, new ResponseData(){statusCode=400, error= new { message="Invalid Params"} } },
                        {Responses.NotFound, new ResponseData(){statusCode =400,error= new { message="method not found", } } },
                        {Responses.ServiceNotFound,new ResponseData(){statusCode= 400, error= new {message="Service Not Found"}} },
                        {Responses.Success, new ResponseData(){statusCode=200, error=new{ success=true } } },
                        {Responses.UnAuthorized, new ResponseData(){statusCode=401, error= new { message="Un Authorize"} } },
                        {Responses.BadRequest, new ResponseData(){ statusCode=400, error=new { message="Bad Request"} } }
                    };
                }
                return _response;
            }
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
        public static void Addwwroot(string filepath, IFormFile file)
        {

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\" + filepath);
            var bytes = new byte[file.Length];
            file.OpenReadStream().Read(bytes, 0, bytes.Length);
            File.WriteAllBytes(path, bytes);
        }
        public static bool Deletewwwroot(string filepath)
        {
            var path = Path.Combine(
                                      Directory.GetCurrentDirectory(),
                                      "wwwroot\\" + filepath);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        #endregion
    }

}
