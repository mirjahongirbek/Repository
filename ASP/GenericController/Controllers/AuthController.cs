using GenericController.Entity;
using GenericController.Modals;
using GenericController.State;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System;

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
        public ResponseData Login([FromBody]LoginViewModal modal)
        {
           var auth= CreateAuth();
            auth.UserName = modal.UserName;
            auth.Password = modal.Password;
           var user= _auth.GetUser(auth);
            if(user== null)
            {
                return this.GetResponse(user);
            }
            return this.GetResponse();
        }
        public ResponseData Lagout()
        {
            return this.GetResponse();
        }
        private IAuth CreateAuth()
        {
            return (IAuth)Activator.CreateInstance(typeof(IAuth));
        }
        #endregion
    }



}
