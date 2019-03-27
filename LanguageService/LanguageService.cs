using RepositoryRule.Entity;
using System.Collections.Generic;
using LanguageService.Interfaces;
using RestClientDotNet;
using LanguageService.State;
using RepositoryRule.Attributes;
using System.Reflection;
using LangEntity;

namespace LanguageService
{
    public class LanguageService<TKey> : ILanguageService<TKey>
        where TKey : class, IEntity<TKey>
    {
        RestClient _client;
        string _project;
        public LanguageService(IEnumerable<IEntity<TKey>> types, string project)
        {
            _project = project;
        }
        public void ParseType(IEntity<TKey> entity)
        {
            var model = entity.GetType();
            var joha = model.GetCustomAttribute<JohaAttribute>();
            Model request = new Model();
            request.EntityModel = joha?.Name ?? model.Name;
            request.Project = _project;
            foreach (var i in model.GetProperties())
            {
               var key=LangState.TT(i);
                if (key.Value != null)
                    request.Fields.Add(key.Key, key.Value);
            }
            
           
        }
    }

}
