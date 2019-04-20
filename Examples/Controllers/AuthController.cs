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

  
    
}
