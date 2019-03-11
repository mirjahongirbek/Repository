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

namespace GenericControllers
{
    //Add Next Log Service
    //Add next GetType changes
    public class GenericController<TKey> : ControllerBase
    {

        BindingFlags bindings = BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public;
        Dictionary<string, Type> _types;
        Dictionary<string, object> _service;
        public GenericController(List<Type> types, List<object> serviceList)
        {
            //_serviceList = serviceList;
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
        }
        #region Props
        [HttpGet]
        public virtual ResponseData GetProps(string id)
        {
            var type = _types[id];
            if (type == null)
            {
                return GetResponse(null, new { });
            }
            Dictionary<string, PropsResult> result = new Dictionary<string, PropsResult>();
            foreach (var i in type.GetProperties())
            {
                var attribute = i.GetCustomAttribute<EntityDescriptionAttribute>();
                PropsResult props = new PropsResult();
                if (attribute == null)
                {
                    props.Type = i.PropertyType.Name.ConvertFront();
                    props.Label = i.Name;
                }
                else
                {
                    props.Type = attribute.FontType.ToString();
                    props.OtherTable = attribute.Name;
                    props.Label = attribute.Label;
                    props.Show = attribute.Show;
                    props.ShowAdd = attribute.ShowAdd;
                }
                result.Add(Char.ToLower(i.Name[0]) + i.Name.Substring(1), props);
            }
            return GetResponse(result);
        }
        [HttpGet]
        public ResponseData GetAllName()
        {
            return GetResponse(_types.Keys);
        }
        #endregion

        #region Get Request List

        [HttpGet]
        public virtual async Task<ResponseData> GetById(TKey id, string name)
        {
            try
            {
                var service = _service.FirstOrDefault(m => m.Key == name).Value;
                if (service == null)
                {
                    GetResponse();
                }
                var type = service.GetType();
                var result = type.InvokeMember("Get", bindings, null, service, new object[] { id, 108, "GetById" });
                return GetResponse(result);
            }
            catch (Exception ext)
            {
                return GetResponse(null, new { });
            }
        }
        [HttpGet]
        public virtual async Task<ResponseData> GetAll(string name)
        {
            try
            {
                var service = _service.FirstOrDefault(m => m.Key == name).Value;
                if (service == null)
                {
                    return GetResponse();
                }
                var type = service.GetType();
                var result = type.InvokeMember("FindAll", bindings, null, service, null);
                return GetResponse(result);
            }
            catch (Exception ext)
            {

                return GetResponse(null, new { });
            }
        }
        #endregion

        #region Post Request List
        [HttpPost]
        public virtual async Task<ResponseData> AddData([FromBody]Request model)
        {
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
                return GetResponse();
            }
            catch (Exception ext)
            {
                return GetResponse(null, new { });
            }
        }

        [HttpPost]
        public virtual async Task<ResponseData> DataWithCount([FromBody] Query model)
        {
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
                return GetResponse(result);
            }
            catch (Exception ext)
            {
                return GetResponse(null, new { });
            }
        }
        [HttpPost]
        public virtual async Task<ResponseData> PostData([FromBody] Query model)
        {
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
                return GetResponse(result);
            }
            catch (Exception ext)
            {
                return GetResponse(null, new { });
            }
        }

        #endregion

        #region Put Request
        [HttpPut]
        public ResponseData UpdateData([FromBody] Request model)
        {
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
                return GetResponse(new SuccesResponse(), null);

            }
            catch (Exception ext)
            {
                return GetResponse();
            }
        }
        #endregion

        #region Delete Requests
        [HttpDelete]
        public virtual async Task<ResponseData> DeleteData(int id, string name)
        {
            try
            {
                return GetResponse();
            }
            catch
            {

                return GetResponse();
            }
        }
        #endregion

        protected virtual ResponseData GetResponse(object data = null, object err = null)
        {
            if (err != null)
            {
                return new ResponseData();
            }

            return new ResponseData() { result = data };
        }
    }


}
