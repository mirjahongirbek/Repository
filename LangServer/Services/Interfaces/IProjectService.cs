using LangEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LangServer.Services.Interfaces
{
  public  interface IProjectService
    {
        //void CreateIndex(string indexName)
         void AddModel(Model model);
        void AddFront(Model model);
        Task Update();
    }
}
