using GenericController.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RepositoryRule.Attributes;
using GenericController.State;
using RepositoryRule.LoggerRepository;
using System.Diagnostics;
using RepositoryRule.Entity;

namespace GenericControllers
{
    //Add Next Log Service
    //Add next GetType changes
    public class GenericController<TKey> : ControllerBase
    {

        BindingFlags bindings = BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public;
        Dictionary<string, Type> _types;
        Dictionary<string, object> _service;
        ILoggerRepository _logger;
        public GenericController(List<Type> types,
            List<object> serviceList, 
            ILoggerRepository logger= null
           
            )
        {
            _service = new Dictionary<string, object>();
            _types = new Dictionary<string, Type>();
            for (var i = 0; i < types.Count; i++)
            {
                var a = types[i].GetCustomAttribute<EntityDescriptionAttribute>();
                if (a != null)
                {
                    _types.Add(a.Name, types[i]);
                    _service.Add(a.Name, serviceList[i]);
                }
                else
                {
                    _types.Add(types[i].Name, types[i]);
                    _service.Add(types[i].Name, serviceList[i]);
                }
            }
            _logger = logger;
        }
        #region Props
        [HttpGet]
        public virtual ResponseData GetProps(string id)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                var type = _types[id];
                if (type == null)
                {
                    return GetResponse(null, new { });
                }
                Dictionary<string, EntityDescriptionAttribute> result = new Dictionary<string, EntityDescriptionAttribute>();
                foreach (var i in type.GetProperties())
                {
                    var attribute = i.GetCustomAttribute<EntityDescriptionAttribute>();
                    
                    if (attribute == null)
                    {
                        EntityDescriptionAttribute attr = new EntityDescriptionAttribute(i.Name);
                        result.Add(Char.ToLower(i.Name[0]) + i.Name.Substring(1), attr);
                    }
                    else
                    {
                        
                        result.Add(Char.ToLower(i.Name[0]) + i.Name.Substring(1), attribute);
                    }
                    
                }
                stop.Stop();
                stop = null;
                return GetResponse(result);
            }
            catch(Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("GetProps", stop.ElapsedMilliseconds, id, ext, "GetProps", code);
                return GetResponse(status:400, code:code);
            }
            
        }
        [HttpGet]
        public ResponseData GetAllName()
        {

            try
            {
                return GetResponse(_types?.Keys);
                
            }
            catch(Exception ext)
            {
                string code = Guid.NewGuid().ToString();
              _logger?.CatchError("GetAll Name", 0, null, ext, "GetAllName", code);
                return GetResponse(status: 400, code:code);
            }
            
        }
        #endregion

        #region Get Request List
        [HttpGet]
        public virtual async Task<ResponseData> GetById(TKey id, string name)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                var service = _service.FirstOrDefault(m => m.Key == name).Value;
                if (service == null)
                {
                    GetResponse();
                }
                var type = service.GetType();
                var result = type.InvokeMember("Get", bindings, null, service, new object[] { id, 108, "GetById" });
                stop.Stop();
                return GetResponse(result);
            }
            catch (Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("Get by Id", stop.ElapsedMilliseconds, new { Id = id, name = name }, ext,"GetById", code);
                return GetResponse(status:400, code:code);
            }
        }
        [HttpGet]
        public virtual async Task<ResponseData> GetAllCount(string id)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return GetResponse();
                }
               var type= _types.FirstOrDefault(m => m.Key == id).Value;
                if(type== null)
                {
                    return GetResponse(status:403);
                }
               var service= _service[id];

              var result= (long)service.GetType().InvokeMember("Count", bindings, null, service, new object[] {0,"GetAllCount" });
                stop.Stop();
                return GetResponse(result);
            }catch(Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("GetAllCount",stop.ElapsedMilliseconds,id, ext, "GetAllCount", code);
              return  GetResponse(status:400, code:code);
            }
        }
        [HttpGet]
        public virtual async Task<ResponseData> GetAll(string id)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {

                var service = _service.FirstOrDefault(m => m.Key == id).Value;
                if (service == null)
                {
                    return GetResponse();
                }
                var type = service.GetType();
                var result = type.InvokeMember("FindAll", bindings, null, service, null);
                stop.Stop();
                return GetResponse(result);
            }
            catch (Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("GetAll", stop.ElapsedMilliseconds, id, ext, "GetAll", code);
                return GetResponse(status:400, code:code);
            }
        }
        #endregion

        #region Post Request List
        [HttpPost]
        public virtual async Task<ResponseData> AddData([FromBody]Request model)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                if (model == null)
                {
                    return GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return GetResponse();
                }
                var result = JsonConvert.DeserializeObject(model.data.ToString(), type, 
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                var service = _service[model.name];
                service.GetType().GetMethod("Add").Invoke(service, new object[] { result, 152, "PostData" });
                stop.Stop();
                return GetResponse();
            }
            catch (Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("Add Data", stop.ElapsedMilliseconds, model, ext, "AddData", code);
                return GetResponse(status:400,code: code);
            }
        }

        [HttpPost]
        public virtual async Task<ResponseData> DataWithCount([FromBody] Query model)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                if (model == null)
                {
                    return GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return GetResponse();
                }
                var service = _service[model.name];
                PostResponse result = new PostResponse();
                if (model.WithOffset)
                {
                    result.items = service.GetType().InvokeMember("FindReverse", bindings, null, service, new object[] { model.key, model.value, model.offset, model.limit });
                    result.count = (long)service.GetType().InvokeMember("Count", bindings, null, service, new object[] { model.key, model.value, 0, "DatawitCount" });
                }
                else
                {
                    result.items = service.GetType().InvokeMember("FindReverse", bindings, null, service, new object[] { model.offset, model.limit, });
                    result.count = (long)service.GetType().InvokeMember("Count", bindings, null, service, new object[] { 0, "PostsData" });
                }
                stop.Stop();
                return GetResponse(result);
            }
            catch (Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("DataWithCount", stop.ElapsedMilliseconds, model, ext, "DataWithCount", code);
                return GetResponse(status:400, code:code);
            }
        }
        [HttpPost]
        public virtual async Task<ResponseData> PostData([FromBody] Query model)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                if (model == null)
                {
                    return GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return GetResponse();
                }
                var service = _service[model.name];
                object result;
                if (model.WithOffset)
                {
                    result = service.GetType().InvokeMember("FindReverse", bindings, null, service, new object[] { model.key, model.value, model.offset, model.limit });

                }
                else
                {
                    result = service.GetType().InvokeMember("FindReverse", bindings, null, service, new object[] { model.key, model.value, });

                }
                stop.Stop();
                return GetResponse(result);
            }
            catch (Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("PostData",stop.ElapsedMilliseconds,model, ext, "PostData", code);
                return GetResponse(status:400, code:code);
            }
        }

        #endregion

        #region Put Request
        [HttpPut]
        public ResponseData UpdateData([FromBody] Request model)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                if (model == null)
                {
                    return GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return GetResponse();
                }
                var result = JsonConvert.DeserializeObject(model.data, type);
                var service = _service[model.name];
                service.GetType().GetMethod("Update").Invoke(service, new object[] { result, 158, "GenericUpdateData" });
                stop.Stop();
                return GetResponse(new SuccesResponse(), null);

            }
            catch (Exception ext)
            {
                stop.Stop();
                string code = Guid.NewGuid().ToString();
                _logger?.CatchError("UpdateData", stop.ElapsedMilliseconds, model, ext, "UpdateData", code);
                return GetResponse(status:400, code:code);
            }
        }
        #endregion

        #region Delete Requests
        [HttpDelete]
        public virtual async Task<ResponseData> DeleteData(int id, string name)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                return GetResponse();
            }
            catch(Exception ext)
            {
               string code= Guid.NewGuid().ToString();
              _logger?.CatchError(code, stop.ElapsedMilliseconds, ext, ext,"DeleteData",code);
                return GetResponse(status:400, code: code);
            }
        }
        #endregion
        protected virtual ResponseData GetResponse(object data = null,   object err = null, int status=200, string code=null)
        {
            if (err != null)
            {
                return new ResponseData();
            }

            return new ResponseData() { result = data };
        }
    }


}
