using LangEntity.Project;
using LangServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using States = LangServer.State.State;
namespace LangServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController:ControllerBase
    {
        IProjectService _project;
        public ProjectController(IProjectService project)
        {
            _project = project;
        }
        public async Task<LangProject> GetProject()
        {
            return States.LangProject;
        }

    }
}
