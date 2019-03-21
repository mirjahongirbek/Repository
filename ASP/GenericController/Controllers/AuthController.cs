using GenericController.Entity;
using GenericController.Modals;
using GenericController.State;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Base;
using RepositoryRule.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GenericControllers.Controllers
{
    public class AuthController<IAuth, IUserRoles, IUserDevice, TKey> : ControllerBase
        where IAuth : class, IAuthUser<TKey, IUserRoles, IUserDevice>
        where IUserRoles : class, IRoleUser<TKey>
        where IUserDevice : class, IUserDevice<TKey>
    {
        Dictionary<string, Type> props;

        #region Header
        protected IAuthRepository<IAuth, IUserRoles, IUserDevice, TKey> _auth;
        protected IUserDeviceRepository<IUserDevice, IUserRoles, TKey> _device;
        protected IRoleRepository<IUserRoles, TKey> _rols;
        bool _checkDevice = false;
        bool _addDeviceIfNew;
        public AuthController(
            IAuthRepository<IAuth, IUserRoles, IUserDevice, TKey> auth,
           IRoleRepository<IUserRoles, TKey> rols,
             IUserDeviceRepository<IUserDevice, IUserRoles, TKey> device,
             bool checkDevice = false,
             bool addDeviceIfNew = true
            )
        {
            _auth = auth;
            _rols = rols;
            _device = device;
            _checkDevice = checkDevice;
            _addDeviceIfNew = addDeviceIfNew;
        }

        public ResponseData GetProps(string name)
        {
            try
            {

            }catch(Exception ext)
            {

            }
          return   this.GetResponse();
        }
        [HttpPost]
        public virtual async Task<ResponseData> Login([FromBody]LoginViewModal modal)
        {
            var auth = CreateAuth();
            auth.UserName = modal.UserName;
            auth.Password = modal.Password;
            if(string.IsNullOrEmpty(modal.DeviceId)|| string.IsNullOrEmpty(modal.DeviceName))
            {
                return this.GetResponse();
            }

            var user = await _auth.GetUser(auth);

            var userDevice= user.DeviceList?.FirstOrDefault(m => m.DeviceId == modal.DeviceId);
            var device = CreateDevice();
            if (userDevice == null&& _addDeviceIfNew)
            {
                device.UserId = user.Id;
                device.DeviceId = modal.DeviceId;
                device.DeviceName = modal.DeviceName;
            }
            


            if (user == null)
            {
                return this.GetResponse(user);
            }
           // var device = CreateDevice();
           // device.UserId = user.Id;
           // device.DeviceId = modal.DeviceId;
           // device.DeviceName = modal.DeviceName;
            
            var result = await _device.LoginAsync(device, user, true);
            return this.GetResponse(result);
        }
        [HttpPost]
        public virtual async Task<ResponseData> Register([FromBody] IAuth model)
        {
            try
            {
                var user = await _auth.GetLoginOrEmail(model.UserName) ?? await _auth.GetLoginOrEmail(model.Email);
                if (user != null)
                {
                    return this.GetResponse();
                }
                if (!await _auth.RegisterAsync(model))
                {
                    return this.GetResponse();
                }
           return     this.GetResponse(true);
            }
            catch (Exception ext)
            {
                return this.GetResponse(ext);
            }
        }
        public ResponseData Lagout()
        {
            return this.GetResponse();
        }
        #region
        private IAuth CreateAuth()
        {
            return (IAuth)Activator.CreateInstance(typeof(IAuth));
        }
        private IUserDevice CreateDevice()
        {
            return (IUserDevice)Activator.CreateInstance(typeof(IUserDevice));
        }
        #endregion
        #endregion
    }



}
