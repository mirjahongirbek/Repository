using LangEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangServer.Services.Interfaces
{
    public  interface IModelService
    {
        Task  Add(Model model);
        Task<Model> GetById(Guid Id, string indexName);
        Task<IEnumerable<Model>> GetAll();
        void CreateIndex(string indexName);
    }
}
