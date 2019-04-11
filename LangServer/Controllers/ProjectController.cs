using LangServer.Services.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Entity;
using SiteResponse;
using System;
using System.Threading.Tasks;
using GenericController.Entity;
using LangServer.Models;
using LangEntity;

namespace LangServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectService _project;
        IEntityService _entity;
        public ProjectController(IProjectService project, IEntityService entity)
        {
            _project = project;
            _entity = entity;
        }
        public async Task<ResponseData> GetProjects()
        {

            return this.GetResponse(State.State.LangProject);
        }

        public async Task<ResponseData> AddLanguage(string name, Guid id, int langId)
        {
            try
            {

                var project = State.State.LangProject.Projects.FirstOrDefault(m => m.Name == name);
                if (project == null) return this.GetResponse();
                var lang = State.State.Languages.FirstOrDefault(m => m.LanguageId == langId);
                if (lang == null) return this.GetResponse();
                project.Langs.Add(lang.Name, lang.LanguageId);
                _project.Update(State.State.LangProject);
                return this.GetResponse(ResponseList.Success);
            }
            catch (Exception ext)
            {
                return this.GetResponse(ext);
            }
        }
        public async Task<ResponseData> GetModal(string name, Guid id, int langId)
        {
            try
            {
                var entity = State.State.LangProject.Projects.FirstOrDefault(m => m.Name == name)?.Entitys.FirstOrDefault(m => m.Id == id);
                return this.GetResponse(await _entity.EntityLang(name, langId, id));
            }
            catch (Exception ext)
            {
                return this.GetResponse(ext);
            }
        }

        [HttpPost]
        public async Task<ResponseData> AddEntity([FromBody] EntityData model)
        {
            try
            {
                Console.WriteLine(model);

                await _entity.AddEntity(model);
                return this.GetResponse(model);
            }
            catch (Exception ext)
            {
                return this.GetResponse(model);
            }

        }
        [HttpGet]
        public async Task<ResponseData> GetFirst(string name, Guid id, int id)
        {
            try
            {
                

            }catch(Exception ext)
            {

            }
        }

    }
}
