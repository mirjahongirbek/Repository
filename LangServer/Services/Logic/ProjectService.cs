using LangEntity.Language;
using LangEntity.Project;
using LangServer.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository.Context;
using System;

namespace LangServer.Services.Logic
{
    public class ProjectService :MongoRepository.MongoRepository<LangProject>, IProjectService
    {
        IMongoCollection<LangProject> _db;
        ILanguageService _language;
        public ProjectService(IMongoContext context,
            ILanguageService language
            ):base(context)
        {
          _db=  context.Database.GetCollection<LangProject>(nameof(LangProject));
            _language = language;
            AddState();
            
        }
        private void AddState()
        {
          var project= GetFirst(m => true);
            if(project== null)
            {
                project = new LangProject() { Id = ObjectId.GenerateNewId().ToString(), CreateTime = DateTime.Now, Name = "joha",  };
                Add(project);
            }
            LangServer.State.State.LangProject = project;
            State.State.Languages = new System.Collections.Generic.List<Language>();
            State.State.Languages.AddRange(_language.FindAll());
        }
       

    }
    public class LanguageService :MongoRepository.MongoRepository<Language>,  ILanguageService
    {
        public LanguageService(MongoRepository.Context.IMongoContext context):base(context)
        {
        }
    }
  
}
