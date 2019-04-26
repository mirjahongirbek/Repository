using RepositoryRule.Entity;
using System.Collections.Generic;
using LanguageService.Interfaces;
using LanguageService.State;
using RepositoryRule.Attributes;
using System.Reflection;
using LangEntity;
using System.Threading.Tasks;
using System;
using LangEntity.Project;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryRule.State;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace LanguageService
{
    public class LanguageService<TKey> : ILanguageService<TKey>
         where TKey : struct
    {
        RestClient _client;
        string _project;
        string modal;
        public LanguageService(IEnumerable<IEntity<TKey>> types, string project = null)
        {
            _project = project ?? "joha";
            _client = State.LangState.Client;
            foreach (var i in types)
            {

              //  ParseType(i);
            }

        }
        public void ParseType(IEntity<TKey> entity)
        {

            var model = entity.GetType();
            var joha = model.GetCustomAttribute<JohaAttribute>();
            TraficcModel request = new TraficcModel();
            request.Id = model.GUID; //.ToString();
            request.ProjectName = _project;
            request.EntityModel = joha?.Name ?? model.Name;
            request.GetFields = new List<Field>();
            foreach (var i in model.GetProperties())
            {
                var key = LangState.TT(i);
                if (key.Value != null)
                    request.GetFields.Add(new Field()
                    {
                        Name = key.Key,
                        Type = key.Value,
                        Value = ""
                    });
            }

            var rest = new RestRequest("/Modal/Add", Method.POST);
            rest.AddJsonBody(request);
            var result = _client.Execute(rest);

        }
        public async Task<List<T>> GetList<T>(int langId)
        {
            try
            {
                var id = typeof(T).GUID;
                var url = "/Project/GetModal?name=" + _project + "&id=" + id.ToString() + "&langId=" + langId;
                var rest = new RestRequest(url, Method.GET);
                try
                {
                    var result = _client.Execute(rest);
                    var res = JsonConvert.DeserializeObject<ResponseData>(result.Content);
                    var list = ((JArray)res.result).ToObject<List<EntityData>>();
                    var results = new List<T>();
                    foreach (var i in list)
                    {
                        results.Add(i.Data.ConvertDictionary<T>());
                    }
                    return results;
                }
                catch (Exception ext)
                {
                    throw ext;
                }

            }
            catch (Exception ext)
            {
                throw ext;
            }

        }
        public async Task<T> Get<T>(int langId, Expression<Func<T, bool>> expression)
            where T : class
        {
            try
            {

            }
            catch (Exception ext)
            {

            }
            return null;
        }
        private List<T> ConvertList<T>(RestRequest request)
        {
            var response = _client.Execute(request);
            var desc = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            return ((JArray)desc.result).ToObject<List<T>>();

        }

   
        public async Task<T> GetById<T>(int langId, int id) where T : class
        {
            try
            {

            }
            catch (Exception ext)
            {

            }
            return null;
        }
        public Task<T> Search<T>(int langId, Expression<Func<T, IComparable>> outExpr, object value) where T : class
        {
            try
            {
                var propertyInfo = ((MemberExpression)outExpr.Body).Member as PropertyInfo;
                if (propertyInfo == null)
                {
                    throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
                }
                SearchViewModal model = new SearchViewModal();
                model.ProjectName = _project;
                var tip = typeof(T);
                model.Id = tip.GUID;
                model.key = propertyInfo.Name;
                model.value = value;
                RestRequest rest = new RestRequest("/ModalType/GetByKey", Method.POST);
                rest.AddJsonBody(model);

               var result= _client.Execute(rest);

            }
            catch(Exception ext)
            {

            }
            return null;
        }
       // public async Task<IEnumerable<T>> Find<T>(int langId, Expression<Func<T, bool>> expression)
       //where T : class
       // {

       //     try
       //     {
       //         var query = LangState.ConvertString<T>(expression);
       //         var tip = typeof(T);
       //         RestRequest rest = new RestRequest("/Modal/Search", Method.POST);
       //         SearchViewModal modal = new SearchViewModal()
       //         {
       //             Id = tip.GUID,
       //             ProjectName = _project,
       //             LangId = langId,


       //         };
       //         rest.AddJsonBody(modal);
       //         List<T> results = new List<T>();
       //         var list = ConvertList<EntityData>(rest);
       //         foreach (var i in list)
       //         {
       //             results.Add(i.Data.ConvertDictionary<T>());
       //         }
       //         return results;
       //     }
       //     catch (Exception ext)
       //     {

       //         throw ext;
       //     }
      // }
    }

}
