using Autofac;
using GenericController.Entity;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RepositoryRule.Attributes;

namespace GenericControllers
{
    public static class Items
    {

        public static T Cast<T>(this Object myobj)
            where T : class, IEntity<int>
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                propertyInfo.SetValue(x, value, null);
            }
            return (T)x;
        }

    }
    public class GenericsController<TKey> : ControllerBase
    {

        BindingFlags bindings = BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public;
        ResponseData GetResponse(object data = null, object err = null)
        {
            if (err != null)
            {
                return new ResponseData();
            }

            return new ResponseData() { result= data};
        }
        Dictionary<string, Type> _types;
        Dictionary<string, object> _service;
        public GenericsController(List<Type> types, List<object> serviceList)
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
        [HttpGet]
        public ResponseData GetProps(string id)
        {
            var type = _types[id];
            if (type == null)
            {
                return GetResponse(null, new { });
            }
            List<string> result = new List<string>();
            foreach (var i in type.GetProperties())
            {
                result.Add(i.Name);
            }
            return GetResponse(result);
        }

        [HttpGet]
        public ResponseData GetAllName()
        {
            return GetResponse(_types.Keys);
        }
        [HttpGet]
        public virtual async Task<ResponseData> GetById(TKey id, string name)
        {
            try
            {
                var service = _service.FirstOrDefault(m => m.Key == name).Value;
                if(service== null)
                {
                    GetResponse();
                }
                var type= service.GetType();
                var result= type.InvokeMember("Get",bindings,null, service, new object[] { id,108,"GetById"});
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
                var service=_service.FirstOrDefault(m => m.Key == name).Value;
                if (service == null)
                {
                    return GetResponse();
                }
                var type = service.GetType();
               var result= type.InvokeMember("FindAll", bindings, null, service, null);
                return GetResponse(result);
            }
            catch (Exception ext)
            {

                return GetResponse(null, new { });
            }
        }
        [HttpPost]
        public virtual async Task<ResponseData> PostData([FromBody]Request model)
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
                var result = JsonConvert.DeserializeObject(model.data.ToString(), type);
                var service = _service[model.name];
                service.GetType().GetMethod("Add").Invoke(service, new object[] { result, 152, "PostData" });
                return GetResponse();
            }
            catch(Exception ext)
            {
                return GetResponse(null, new {  });
            }
          
        }
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

    }

    public class Query
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
