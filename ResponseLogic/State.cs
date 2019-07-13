using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using RepositoryRule.Entity;
using ValidRepository.Execption;

namespace SiteResponse
{
    public static class State
    {
        public static bool HasMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }

        public static int GetLang(this ControllerBase control, int defaultValue = 0)
        {
            try
            {
                var result = (string) control.Request.Headers.FirstOrDefault(m => m.Key == "Accept-Language").Value;
                if (string.IsNullOrEmpty(result)) return defaultValue;
                return Convert.ToInt32(result);
            }
            catch (Exception ext)
            {
            }

            return 0;
        }

        public static ResponseData GetResponse<TKey>(this ControllerBase cBase, ValidException valid)
            where TKey : struct
        {
            if (cBase.GetLang() == 0)
            {
                cBase.Response.StatusCode = 400;
                return new ResponseData {error = valid};
            }

            return Valid(valid);
        }

        public static ResponseData GetResponse(this ControllerBase cBase, ModelStateDictionary modelState)
        {
            var result = new ResponseData();
            var message = "";
            foreach (var state in modelState)
            {
                if (state.Value.ValidationState != ModelValidationState.Invalid) continue;
                message += "\r\n" + state.Key + "не соответствует";
            }

            result.error = new {message};
            return result;
        }

        public static ResponseData GetResponse(this ControllerBase cBase, object result, Responses responses)
        {
            if (responses == Responses.Ok || responses == Responses.Success) return new ResponseData {result = result};
            return new ResponseData {error = result};
        }

        public static ResponseData GetResponse(this ControllerBase cBase,
            object result = null,
            int status = 200,
            object error = null)
        {
            if (error != null)
            {
                cBase.Response.StatusCode = status == 200 ? 400 : status;
                return new ResponseData
                {
                    error = error
                };
            }

            #region

            if (result is ValidException valid)
            {
                cBase.Response.StatusCode = valid.HttpStatusCode;
                Valid(valid);
            }

            #endregion

            if (result is Exception exception)
            {
                cBase.Response.StatusCode = 400;
                return new ResponseData {error = new {message = exception.Message}};
            }

            if (result is ResponseData response)
            {
                response.statusCode = response.statusCode == 0 ? 200 : response.statusCode;
                cBase.Response.StatusCode = response.statusCode;
                return response;
            }

            if (result is Responses responses) return GetResponse(cBase, responses);
            return new ResponseData {result = result};
        }

        public static ResponseData GetResponse(this ControllerBase cBase, object result, object err)
        {
            if (err != null)
            {
                cBase.Response.StatusCode = 400;
                return new ResponseData
                {
                    error = err
                };
            }

            return GetResponse(cBase, result, 200, err);
        }

        public static ResponseData GetResponse(this ControllerBase cBase, ResponseData response)
        {
            if (response.Responses != null) return GetResponse(cBase, response.Responses);
            return response;
        }

        public static ResponseData GetResponse(this ControllerBase cBase, Responses responses)
        {
            var result = ResponseList.FirstOrDefault(m => m.Key == responses);
            if (result.Value == null)
            {
                cBase.Response.StatusCode = 400;
                return new ResponseData {error = new {message = "result not implament"}};
            }

            cBase.Response.StatusCode = result.Value.statusCode;
            return result.Value;
        }


        private static ResponseData Valid(ValidException valid)
        {
            if (valid.Validators != null)
                return new ResponseData
                {
                    error = valid.Validators
                };
            if (valid.Validator != null)
                return new ResponseData
                {
                    error = valid.Validator
                };
            if (valid.Error != null)
                return new ResponseData
                {
                    error = valid.Error
                };
            return new ResponseData
            {
                error = new
                {
                    message = "Http Status Code " + valid.HttpStatusCode
                }
            };
        }

        #region

        private static Dictionary<Responses, ResponseData> _response;

        private static Dictionary<Responses, ResponseData> ResponseList
        {
            get
            {
                if (_response == null)
                    _response = new Dictionary<Responses, ResponseData>
                    {
                        {Responses.Ok, new ResponseData {statusCode = 200, result = new {success = true}}},
                        {
                            Responses.ModelIsNull,
                            new ResponseData {statusCode = 400, error = new {message = "Model is Null"}}
                        },
                        {Responses.Conflict, new ResponseData {statusCode = 400, error = new {message = "conflict"}}},
                        {
                            Responses.InvalidParameters,
                            new ResponseData {statusCode = 400, error = new {message = "Invalid Params"}}
                        },
                        {
                            Responses.NotFound,
                            new ResponseData {statusCode = 400, error = new {message = "method not found"}}
                        },
                        {
                            Responses.ServiceNotFound,
                            new ResponseData {statusCode = 400, error = new {message = "Service Not Found"}}
                        },
                        {Responses.Success, new ResponseData {statusCode = 200, result = new {success = true}}},
                        {
                            Responses.UnAuthorized,
                            new ResponseData {statusCode = 401, error = new {message = "Un Authorize"}}
                        },
                        {
                            Responses.BadRequest,
                            new ResponseData {statusCode = 400, error = new {message = "Bad Request"}}
                        }
                    };
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
            if (RepositoryRule.State.State.IsDevelopment) return control.GetResponse(ext.Message);
            var code = Guid.NewGuid().ToString();

            return control.GetResponse();
        }

        public static void Addwwroot(string filepath, IFormFile file)
        {
            var filelist = filepath.Split('\\');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\");
            if (filelist.Count() > 1)
            {
                var str = "";
                for (var i = 0; i < filelist.Count(); i++)
                {
                    if (filelist.Count() - 1 == i) break;
                    if (i == 0)
                        str = filelist[i];
                    else
                        str += "\\" + filelist[i];
                    if (!Directory.Exists(path + str)) Directory.CreateDirectory(path + str);
                }
            }

            path = path + "\\" + filepath;
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