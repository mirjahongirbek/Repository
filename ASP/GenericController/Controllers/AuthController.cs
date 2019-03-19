using GenericController.Entity;
using GenericController.Modals;
using GenericController.State;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System;
using System.Threading.Tasks;

namespace GenericControllers.Controllers
{
    public class AuthController<IAuth, IUserRoles, IUserDevice, TKey> : ControllerBase
        where IAuth : class, IAuthUser<TKey>
        where IUserRoles : class, IRoleUser<TKey>
        where IUserDevice : class, IUserDevice<TKey>
    {
        #region Header
       protected IAuthRepository<IAuth, TKey> _auth;
       protected IUserDeviceRepository<IUserDevice, TKey> _device;
       protected IRoleRepository<IUserRoles, TKey> _rols;
        public AuthController(
            IAuthRepository<IAuth, TKey> auth,
            IUserDeviceRepository<IUserDevice, TKey> device,
            IRoleRepository<IUserRoles, TKey> rols
            )
        {
            _auth = auth;
            _rols = rols;
            _device = device;
        }

        [HttpPost]
        public async Task<ResponseData> Login([FromBody]LoginViewModal modal)
        {
           var auth= CreateAuth();
            auth.UserName = modal.UserName;
            auth.Password = modal.Password;
           var user= await _auth.GetUser(auth);
            if(user== null)
            {
                return this.GetResponse(user);
            }
           var device= CreateDevice();
            device.DeviceId = modal.DeviceId;
            device.DeviceName = modal.DeviceName;
          var result= await _device.LoginAsync(device, user);
            return this.GetResponse(result);
        }
        public ResponseData Lagout()
        {
            return this.GetResponse();
        }
        private IAuth CreateAuth()
        {
            return (IAuth)Activator.CreateInstance(typeof(IAuth));
        }
        private IUserDevice CreateDevice()
        {
            return (IUserDevice)Activator.CreateInstance(typeof(IUserDevice));
        }
        #endregion
    }



}
