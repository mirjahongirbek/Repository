using Microsoft.AspNetCore.Mvc;


namespace LangServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
