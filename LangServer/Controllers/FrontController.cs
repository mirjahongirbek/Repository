
using LangEntity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LangServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FrontController : ControllerBase
    {
        [HttpGet]
        public object GetAll()
        {
            return null;
        }
        [HttpGet]
        public FrontResult GetBy(string name)
        {

        }
        

    }
}
