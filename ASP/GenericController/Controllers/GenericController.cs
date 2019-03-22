using GenericController.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using RepositoryRule.Attributes;
using RepositoryRule.LoggerRepository;
using System.Diagnostics;
using RepositoryRule.Base;
using RepositoryRule.State;
using GenericController.State;
using RState = RepositoryRule.State.State;


namespace GenericControllers.Controllers
{
    //Add Next Log Service
    //Add next GetType changes
    //modalviewModal
    //anguage
    public class GenericController<TKey> : ControllerBase
    {

        BindingFlags bindings = BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public;
        Dictionary<string, Type> _types;
        Dictionary<string, object> _service;
        ILoggerRepository _logger;
        IEnumerable<IControllerCommand<TKey>> _commands;
        public GenericController(List<Type> types,
            List<object> serviceList,
            IEnumerable<IControllerCommand<TKey>> commands = null
            )
        {
            if (commands != null)
            {
                _commands = commands;
            }
            _service = new Dictionary<string, object>();
            _types = new Dictionary<string, Type>();
            for (var i = 0; i < types.Count; i++)
            {
                var a = types[i].GetCustomAttribute<PropsAttribute>();
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
            //_logger = logger;
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
                    return this.GetResponse();
                }
                Dictionary<string, PropsAttribute> result = new Dictionary<string, PropsAttribute>();
                foreach (var i in type.GetProperties())
                {
                    var attr=i.GetProps();
                    if(attr!= null)
                    {
                        result.Add(Char.ToLower(i.Name[0]) + i.Name.Substring(1), attr);
                    }
                }
                stop.Stop();
                stop = null;
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
            }
        }
        [HttpGet]
        public ResponseData GetAllName()
        {
            try
            {
                return this.GetResponse(_types?.Keys);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, Stopwatch.StartNew());
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
                    this.GetResponse();
                }
                var type = service.GetType();
                var result = type.InvokeMember("Get", bindings, null, service, new object[] { id, 108, "GetById" });
                stop.Stop();
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
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
                    return this.GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == id).Value;
                if (type == null)
                {
                    return this.GetResponse(status: 403);
                }
                var service = _service[id];
                var result = (long)service.GetType().InvokeMember("Count", bindings, null, service, new object[] { 0, "GetAllCount" });
                stop.Stop();
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
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
                    return this.GetResponse();
                }
                var type = service.GetType();
                var result = type.InvokeMember("FindAll", bindings, null, service, null);
                stop.Stop();
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
                
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
                    return this.GetResponse(); // TODO change
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return this.GetResponse(); //TODO change
                }
                var result = model.data.SerializeMe(type);
                var service = _service[model.name];
                var errlist = result.CheckJwt(User);

                if (errlist.Count > 0)
                {
                    return this.GetResponse(errlist);
                }

                var command = _commands.FirstOrDefault(m => m.Name == model.name);
                await command.Add(result, User);
                service.GetType().GetMethod("Add").Invoke(service, new object[] { result, 152, "PostData" });
                stop.Stop();
                return this.GetResponse();
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop, model);
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
                    return this.GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return this.GetResponse();
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
                    RState.ListDataParse(result.items, type);
                    result.count = (long)service.GetType().InvokeMember("Count", bindings, null, service, new object[] { 0, "PostsData" });
                }
                stop.Stop();
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
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
                    return this.GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return this.GetResponse();
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
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
                //stop.Stop();
                //string code = Guid.NewGuid().ToString();
                //_logger?.CatchError("PostData", stop.ElapsedMilliseconds, model, ext, "PostData", code);
                //return this.GetResponse(status: 400, code: code);
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
                    return this.GetResponse();
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return this.GetResponse();
                }
                var result = model.data.SerializeMe(type);
                var service = _service[model.name];
                service.GetType().GetMethod("Update").Invoke(service, new object[] { result, 158, "GenericUpdateData" });
                stop.Stop();
                return this.GetResponse(new SuccesResponse());

            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
            }
        }
        #endregion

        #region Delete Requests
        [HttpDelete]
        public virtual async Task<ResponseData> DeleteData(TKey id, string name)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {

                return this.GetResponse();
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
               
            }
        }
        #endregion
                
    }

}
//string code = Guid.NewGuid().ToString();
//_logger?.CatchError(code, stop.ElapsedMilliseconds, ext, ext, "DeleteData", code);
//return this.GetResponse(status: 400, code: code);