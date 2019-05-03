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
using RState = RepositoryRule.State.State;
using RepositoryRule.Entity;
using SiteResponse;
using System.Collections;
using GenericController.Store;

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
                    _types.Add(a.Name.ToLower(), types[i]);
                    _service.Add(a.Name.ToLower(), serviceList[i]);
                }
                else
                {
                    _types.Add(types[i].Name.ToLower(), types[i]);
                    _service.Add(types[i].Name.ToLower(), serviceList[i]);
                }
            }
            //_logger = logger;
        }
        #region Props
        private Type GetType(string id) { return _types.FirstOrDefault(m => m.Key.ToLower() == id.ToLower()).Value; }
        public object GetService(string id) { return _service.FirstOrDefault(m => m.Key.ToLower() == id.ToLower()).Value; }
        [HttpGet]
        public virtual ResponseData GetProps(string id)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                var type = GetType(id);
                if (type == null)
                {
                    return this.GetResponse(Responses.ServiceNotFound);
                }
                Dictionary<string, PropsAttribute> result = new Dictionary<string, PropsAttribute>();
                foreach (var i in type.GetProperties())
                {
                    var attr = i.GetProps();
                    if (attr != null)
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
        public virtual ResponseData GetForegnKey(string id)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                var tip = GetType(id);
                var attr = tip.GetCustomAttribute<JohaAttribute>();
                var service = GetService(id);
                var serviceType = service.GetType();
                if (attr == null)
                {

                }
                var result = (IEnumerable)serviceType.InvokeMember("FindAll", bindings, null, service, null);
                stop.Stop();
                return this.GetResponse(result);

            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
            }

        }
        [HttpGet]
        public virtual ResponseData GetAllName()
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
            name = name.ToLower();
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return this.GetResponse(Responses.ModelIsNull);
                }
                var service = GetService(name);
                if (service == null)
                {
                    this.GetResponse(Responses.ServiceNotFound);
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
                    return this.GetResponse(Responses.ModelIsNull);
                }

                var type = GetType(id);
                if (type == null)
                {
                    return this.GetResponse(Responses.ServiceNotFound);
                }
                var service = GetService(id);
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
                var service = GetService(id);//_service.FirstOrDefault(m => m.Key == id).Value;
                if (service == null)
                {
                    return this.GetResponse();
                }
                var type = service.GetType();
                var result = type.InvokeMember("FindAll", bindings, null, service, null);
                RState.ListDataParse(result, type);
                stop.Stop();
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);

            }
        }
        [HttpGet]
        public virtual async Task<ResponseData> GetViewModal(int id, string name)
        {
            try
            {
                name = name.ToLower();
                var model = InternalStore.Modals.FirstOrDefault(m => m.Name == name && m.Actions.Contains((RepositoryRule.Enums.Actions)id));
                if (model == null)
                {
                   return GetProps(name);
                }
                var tip=   _types.FirstOrDefault(m => m.Key.ToLower() == name);
               return this.GetResponse(InternalStore.ParseModalProps(model, tip.Value));

            }
            catch (Exception ext)
            {
                return this.GetResponse(ext);
            }
        }
        #endregion

        #region Post Request List
        [HttpPost]
        public virtual async Task<ResponseData> AddData([FromBody]Request model)
        {
            var stop = Stopwatch.StartNew();
            try
            {
                if (model == null)
                {
                    return this.GetResponse(Responses.ModelIsNull); // TODO change
                }
                var type = GetType(model.name);
                if (type == null)
                {
                    return this.GetResponse(Responses.ServiceNotFound); //TODO change
                }
                var result = model.data.SerializeMe(type);
                var service = GetService(model.name); //_service[model.name];
                var errlist = result.CheckJwt(User);
                if (errlist.Count > 0)
                {
                    return this.GetResponse(errlist);
                }

                var command = _commands?.FirstOrDefault(m => m.Name == model.name);
                if (command != null)
                {
                    await command.Add(result, User);
                }
                service.GetType().GetMethod("Add").Invoke(service, new object[] { result, 152, "PostData" });
                stop.Stop();
                return this.GetResponse(result);
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
                    return this.GetResponse(Responses.ModelIsNull);
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return this.GetResponse(Responses.ServiceNotFound);
                }
                var service = _service[model.name];
                var result = new PostResponse();
                if (model.WithOffset)
                {

                    result.Items = (IEnumerable)service.GetType().InvokeMember("FindReverse", bindings, null, service, new object[] { model.key, model.value, model.offset, model.limit });
                    RState.ListDataParse(result.Items, type);
                    result.Count = (long)service.GetType().InvokeMember("Count", bindings, null, service, new object[] { model.key, model.value, 0, "DatawitCount" });
                }
                else
                {
                    result.Items = (IEnumerable)service.GetType().InvokeMember("FindReverse", bindings, null, service, new object[] { model.offset, model.limit, });
                    RState.ListDataParse(result.Items, type);
                    result.Count = (long)service.GetType().InvokeMember("Count", bindings, null, service, new object[] { 0, "PostsData" });
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
                    return this.GetResponse(Responses.ModelIsNull);
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return this.GetResponse(Responses.ServiceNotFound);
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
               
            }
        }
        #endregion

        #region Put Request
        [HttpPut]
        public virtual ResponseData UpdateData([FromBody] Request model)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                if (model == null)
                {
                    return this.GetResponse(Responses.ModelIsNull);
                }
                var type = _types.FirstOrDefault(m => m.Key == model.name).Value;
                if (type == null)
                {
                    return this.GetResponse(Responses.ServiceNotFound);
                }
                var result = model.data.SerializeMe(type);
                var errlist = result.CheckJwt(User);

                if (errlist.Count > 0)
                {
                    return this.GetResponse(errlist);
                }
                var service = _service[model.name];
                service.GetType().GetMethod("Update").Invoke(service, new object[] { result, 158, "GenericUpdateData" });
                stop.Stop();
                return this.GetResponse(Responses.Success);

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
                var tip = _types.FirstOrDefault(m => m.Key == name).Value;
                if (tip == null)
                {
                    return this.GetResponse(Responses.ServiceNotFound);
                }
                var service = _service[name];
                var result = service.GetType().InvokeMember("Delete", bindings, null, service, new object[] { id });
                return this.GetResponse(Responses.Success);
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