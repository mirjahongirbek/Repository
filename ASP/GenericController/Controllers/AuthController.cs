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
using RepositoryRule.State;
using RepositoryRule.Attributes;
using System.Reflection;
using System.Diagnostics;

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

        [HttpGet]
        public ResponseData GetMe()
        {
            var idtip  =CreateAuth().GetType().GetProperty("Id");
            //var idtip = CreateAuth().Id.GetType();
            var auth = idtip.GetCustomAttribute<AuthAttribute>();
            var name = "";
            name = auth?.Name ?? idtip.Name;
            name = User.FindFirst(name).Value;
            return this.GetResponse(_auth.GetMe(name));
        }
        
        [HttpGet]
        public ResponseData GetProps(int id)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                Type type;
                switch (id)
                {
                    case 1: {type = typeof(IAuth);} break;
                    case 2: { type = typeof(IUserRoles);}break;
                    case 3: { type = typeof(IUserDevice); }break;
                    default: { type = null; } break;
                }
                if (type == null)
                {
                    return this.GetResponse();
                }
                Dictionary<string, PropsAttribute> result = new Dictionary<string, PropsAttribute>();
                foreach (var i in type.GetProperties())
                {
                    var attr = i.GetProps();
                    if (attr != null)
                    {
                        result.Add(Char.ToLower(i.Name[0]) + i.Name.Substring(1), attr);
                    }
                }
                stop.Stop();
                stop = null;
                return this.GetResponse(result);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
            }

           
        }
        [HttpPost]
        public virtual async Task<ResponseData> Login([FromBody]LoginViewModal modal)
        {
            Stopwatch stop = Stopwatch.StartNew();
            try
            {
                var auth = CreateAuth();
                auth.UserName = modal.UserName;
                auth.Password = modal.Password;
                var userAgent = Request.Headers["User-Agent"];
                if (string.IsNullOrEmpty(userAgent))
                {
                    this.GetResponse(err: new { Message = "User Agent not Exsist" });
                }
                if (string.IsNullOrEmpty(modal.DeviceId))
                {
                    return this.GetResponse();
                }
                var user = await _auth.GetUser(auth);
                if (user == null)
                {
                    return this.GetResponse();
                }
                var userDevice = user.DeviceList?.FirstOrDefault(m => m.DeviceId == modal.DeviceId);
                if (userDevice == null && _addDeviceIfNew)
                {
                    userDevice = CreateDevice();
                    userDevice.UserId = user.Id;
                    userDevice.DeviceId = modal.DeviceId;
                    userDevice.DeviceName = userAgent;
                    await _device.Add(userDevice);
                }
                var getClaim = user.CreateClaim();
                var result = EntityRepository.State.State.GetAuth(getClaim, userDevice);
                userDevice.AccessToken = result.AccessToken;
                userDevice.RefreshToken = result.RefreshToken;
                await _device.Update(userDevice);
                return this.GetResponse(result);
            }
            catch(Exception ext)
            {
                return this.ExceptionResult(ext, stop);
            }
          
        }
        [HttpPost]
        public virtual async Task<ResponseData> Register([FromBody] IAuth model)
        {
            Stopwatch stop = Stopwatch.StartNew();
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
                stop.Stop();
                return this.GetResponse(true);
            }
            catch (Exception ext)
            {
                return this.ExceptionResult(ext, stop);
            }
        }
        public ResponseData Lagout()
        {
            _device.Logout(Request.Headers["Authorization"]);
            return this.GetResponse();
        }
        #region Private Methods
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
