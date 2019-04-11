using RepositoryRule.Entity;
using System.Collections.Generic;
using LanguageService.Interfaces;
using RestClientDotNet;
using LanguageService.State;
using RepositoryRule.Attributes;
using System.Reflection;
using LangEntity;
using System.Threading.Tasks;
using System.Collections;
using System;
using LangEntity.Project;

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
            modal = "/api/Modal";
            foreach (var i in types)
            {

                ParseType(i);
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
            Console.WriteLine(modal + "/Add");
            var result = _client.PostAsync<RepositoryRule.Entity.ResponseData, TraficcModel>(request, modal + "/Add").Result;
        }
        public async Task<List<T>> GetList<T>(int langId)
        {
            try
            {
               var id= typeof(T).GUID;


            }catch(Exception ext)
            {

            }
            return null;
        }
        public async Task<T> Get<T>(int langId, Dictionary<string, object> list)
        {
            try
            {

            }
            catch (Exception ext)
            {

            }
        }
        public async Task<T> GetById<T>(int langId, int id)
        {
            try
            {

            }
            catch (Exception ext)
            {

            }
        }
        
        public async Task GetEntity(IEntity<TKey> entity, string lang)
        {
            try
            {

            }
            catch (Exception ext)
            {

            }

        }


    }

}
