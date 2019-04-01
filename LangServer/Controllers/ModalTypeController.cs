using LangEntity;
using LangServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using States = LangServer.State.State;
namespace LangServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModalController : ControllerBase
    {
        IModelService _model;
        public ModalController(IModelService model)
        {
            _model = model;
        }
        #region Get
        [HttpGet]
        public async Task<bool> ExsistProject(string Project)
        {
            return States.Client.IndexExists(Project).Exists;
        }
        public async Task<IEnumerable<Model>> GetAll()
        {
           return await _model.GetAll();
        }
        #endregion
        
        
        #region

        [HttpPost]
        public async Task<ResponseData> Add([FromBody] Model model)
        {
            await _model.Add(model);
            return null;
        }
        #endregion
    }
}
