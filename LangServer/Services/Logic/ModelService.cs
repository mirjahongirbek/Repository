using LangEntity;
using LangServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using States = LangServer.State.State;

namespace LangServer.Services.Logic
{
    public class ModelService : IModelService
    {
        Nest.ElasticClient _client;
        IProjectService _project;
        public ModelService(IProjectService project)
        {
            _client = States.Client;
            _project = project;
        }
        public async Task Add(Model model)
        {
            model.ProjectName = model.ProjectName.ToLower();
            CreateIndex(model.ProjectName);
            var data = await GetById(model.Id, model.ProjectName);
            if (data == null)
            {
                await States.Client.IndexAsync(model, m => m.Index(model.ProjectName).Type("model"));
                _project.AddModel(model);
            }
                


        }

        public void CreateIndex(string indexName)
        {
            if (!States.Client.IndexExists(indexName.ToLower()).Exists)
            {
                _client.CreateIndex(indexName.ToLower());
            }
        }

        public Task<IEnumerable<Model>> GetAll()
        {
            return null;
        }

        public async Task<Model> GetById(Guid Id, string indexName)
        {
            return _client.Search<Model>(m => m.Index(indexName).Query(q => q.Ids(n => n.Values(Id)))).Documents.FirstOrDefault();
            //return (await _client.SearchAsync<Model>(m => m.Index(indexName).Query(qq => qq.Match(n => n.Field(f => f.Id).Query(Id))).Size(1))).Documents.FirstOrDefault();
        }


    }
}
