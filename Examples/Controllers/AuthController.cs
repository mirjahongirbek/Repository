using ServiceList;

using Entity;
using GenericControllers.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Examples.Controllers
{
    //[Route("/api/[controller]/[action]")]
    public class AuthController:AuthController<Entity.User,RoleUser, UserDevice, int>
    {
      
        public AuthController(
            IAuthUserService user,
            IRoleService role,
            IUserDeviceService device
           
            ) : base(user, role, device)
        {
            //_checkDevice = checkDevice;
        }
    }

    //public class HomeController: GenericController<string>
    //{
    //   // readonly IDataService _data;
    //    public HomeController(IDataService data
        
    //       )
    //        : base(new List<Type>() {typeof(Data)},new List<object>() { data,  })
    //    {
            
    //    }
    //    public IActionResult Index()
    //    {
          
    //        return Ok();
    //    }

    //    public IActionResult Privacy()
    //    {
    //        var claims = new List<Claim>
    //            {
    //                new Claim(ClaimsIdentity.DefaultNameClaimType, ""),
    //                new Claim(ClaimsIdentity.DefaultRoleClaimType, "sd")
    //            };

    //        return Ok();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return Ok();
    //    }
    //}
    
}
