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
                //Task.Run(() =>
                //{
                //    ParseType(i);
                //});
                //ParseType(i).Start();
            }

        }
        public void ParseType(IEntity<TKey> entity)
        {

            var model = entity.GetType();
            var joha = model.GetCustomAttribute<JohaAttribute>();
            Model request = new Model();
            request.Id = model.GUID;
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
            var result = _client.PostAsync<ResponseData, Model>(request, modal + "/Add").Result;





        }
        public async Task GetEntity(IEntity<TKey> entity, string lang)
        {

        }

    }

}
