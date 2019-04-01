using LangEntity;
using LangEntity.Project;
using LangServer.Services.Interfaces;
using Nest;
using System;
using System.Linq;
using System.Threading.Tasks;
using States = LangServer.State.State;

namespace LangServer.Services.Logic
{
    public class ProjectService : IProjectService
    {
        public LangProject _project;
        public ElasticClient _client;
        public ProjectService()
        {
            States.ProjectName = (States.ProjectName ?? "LangProject").ToLower();
            _project = States.Client.Get<LangProject>(1, m => m.Index(States.ProjectName)).Source;
            if (_project == null)
            {
                States.LangProject=CreateLang();
            }
            States.LangProject = _project;
            _client = State.State.Client;
        }

        public void AddModel(Model model)
        {
            var getProject=_project.Projects.FirstOrDefault(m => m.Name == model.ProjectName);
            
            if(getProject== null)
            {
                _project.Projects.Add(new Projects()
                {
                    Id = Guid.NewGuid(),
                    Name = model.ProjectName,
                    Entitys = new System.Collections.Generic.Dictionary<string, string>() { { model.Id.ToString(), model.EntityModel } }
                });
                Update();
                return;
            }
            getProject.Entitys.Add(model.Id.ToString(), model.EntityModel);

            Update();
        }
        public void AddFront(Model model)
        {
            var frontProject = _project.FrontProjects.FirstOrDefault(m => m.Name == model.ProjectName);

            if (frontProject == null)
            {
                _project.FrontProjects.Add(new Projects()
                {
                    Id = Guid.NewGuid(),
                    Name = model.ProjectName,
                    Entitys = new System.Collections.Generic.Dictionary<string, string>() { { model.Id.ToString(), model.EntityModel } }
                });
                Update();
                return;
            }
            //frontProject.Entitys.Add(model.Id.ToString(), model.EntityModel);
            //Update();

        }
       public async Task Update()
        {
            //_client.Update
           await _client.UpdateAsync(
            new DocumentPath<LangProject>(_project.Id), u =>
                 u.Index(States.ProjectName).Type("langproject").Doc(_project));
           
        }
      
        
        private LangProject CreateLang()
        {
            _project = new LangProject();
            _project.CreateTime = System.DateTime.Now;
            _project.Name = States.ProjectName;
            _project.Id = 1;
            _project.FrontProjects = new System.Collections.Generic.List<Projects>();
            _project.Projects = new System.Collections.Generic.List<Projects>();
            var settings = new IndexSettings();
            settings.NumberOfReplicas = 1;
            settings.NumberOfShards = 5;
           // settings.Settings.Add("merge.policy.merge_factor", "10");
            //settings.Settings.Add("search.slowlog.threshold.fetch.warn", "1s");
          // var ss= States.Client.CreateIndex(State.State.ProjectName,m=>m.Index(State.State.ProjectName));
           var sddsf= States.Client.Index<LangProject>(_project, m => m.Id(_project.Id)
            .Index(States.ProjectName)
            .Type("langproject")
            );
            return _project;


        }

    }
  
}
