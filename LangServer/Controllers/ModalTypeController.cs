using LangEntity;
using LangServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using States = LangServer.State.State;
using LangServer.State;
using System.Linq;
using System;

namespace LangServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModalController : ControllerBase
    {
       
        IProjectService _project;

        public ModalController(
            IProjectService project
            )
        {
            
            _project = project;
        }

        #region Get
        [HttpGet]
        public async Task<bool> ExsistProject(string Project)
        {
           var project= States.LangProject.Projects.FirstOrDefault(m => m.Name == Project);
            if(project== null)
            {
                return false;
            }
            return true;
            //return States.LangProject.Projects.FirstOrDefault(m => m.Name == Project) ? true : false;
            
        }
        
        #endregion
        
        
        public ResponseData GetByDict(string Project)
        {
            return null;
        }
        #region 
        [HttpPost]
        public async Task<ResponseData> Add([FromBody] TraficcModel model)
        {
            try
            {
                var project = State.State.LangProject.Projects.FirstOrDefault(m => m.Name == model.ProjectName);
                bool isUpdate = false;
                if (project == null)
                {
                    project = new LangEntity.Project.Projects()
                    { Id = model.Id, Name = model.ProjectName };
                    isUpdate = true;
                    States.LangProject.Projects.Add(project);
                }
                var entity = project.Entitys.FirstOrDefault(m => m.Id == model.Id);
                if (entity == null)
                {
                    isUpdate = true;
                    entity = new LangEntity.Project.Entity();
                    entity.Id = model.Id;
                    entity.Name = model.EntityModel;
                    entity.GetFields = model.GetFields;
                    project.Entitys.Add(entity);
                }
                if (isUpdate)
                {
                    _project.Update(States.LangProject);
                }
                return null;
            }
            catch(Exception ext)
            {

            }
            return null;
           
        }
        #endregion
    }
}
