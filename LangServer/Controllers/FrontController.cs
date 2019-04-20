
using LangEntity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FrontController : ControllerBase
    {
        public FrontController()
        {

        }
        [HttpGet]
        public object GetAll()
        {
            return null;
        }
        //[HttpGet]
        //public async Task<bool> ExsistProject(string name)
        //{
        //    return State.State.Client.IndexExists("front" + name).Exists;
        //}

        [HttpGet]
        public FrontResult GetBy(string name)
        {
            return null;
        }
        

    }
}
