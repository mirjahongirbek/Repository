using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entity;
using EntityRepository.Context;
using EntityRepository.Repository;
using EntityRepository.State;
using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace ServiceList
{
    public interface ICompanyService : IRepositoryBase<Company, int>
    {

    }
    public interface IProductService : IRepositoryBase<Product, int>
    {

    }
    public interface ICategoryService:IRepositoryBase<Category, int>
    {

    }
    public class CategoryService : EntityRepository.SqlRepository<Category>, ICategoryService
    {
        public CategoryService(IDataContext context) : base(context)
        {
        }
    }
    public class CompanyService : EntityRepository.SqlRepository<Company>, ICompanyService
    {
        public CompanyService(IDataContext context) : base(context)
        {
        }
    }
    public class ProductService : EntityRepository.SqlRepository<Product>, IProductService
    {
        public ProductService(IDataContext context) : base(context)
        {
        }
    }
    #region
    public interface IAuthUserService : IAuthRepository<Entity.User, Entity.RoleUser, Entity.UserDevice, int>
    {

    }
    public class AuthUserService : AuthRepository<User, RoleUser, UserDevice>, IAuthUserService
    {
        public AuthUserService(IDataContext context) : base(context)
        {
        }
    }
    #endregion
    #region
    public interface IRoleService:IRoleRepository<Entity.RoleUser, int>
    {

    }
    public class RoleService: RoleRepository<Entity.RoleUser>, IRoleService
    {

    }
    #endregion
    #region
    public interface IUserDeviceService: IUserDeviceRepository<Entity.UserDevice, Entity.RoleUser, int>
    {

    }
    public class UserDeviceService : EntityRepository.Repository.DeviceRepository<Entity.UserDevice, Entity.RoleUser>, IUserDeviceService
    {
        public UserDeviceService(IDataContext context) : base(context)
        {
        }

        //public override async Task<AuthResult> LoginAsync(UserDevice model, IAuthUser<int, RoleUser, UserDevice> user)
        //{
        //    try
        //    {
                
        //        var claims = GetIdentity(user.UserName, user.Roles?.ToList());
        //        var authResult = State.GetAuth(claims, model);
        //        return authResult;
        //    }
        //    catch (Exception ext)
        //    {
        //        throw new Exception(ext.Message, ext);
        //    }
        //  //  return base.LoginAsync(model, user);
        //}
        //public    ClaimsIdentity GetIdentity(string username, List<RoleUser> rols)
        //{
        //    var claims = new List<Claim>
        //        {
        //            new Claim(ClaimsIdentity.DefaultNameClaimType, username),
        //        };
        //    if (rols != null)
        //        foreach (var i in rols)
        //        {
        //            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, i.Name));
        //        }

        //    ClaimsIdentity claimsIdentity =
        //    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
        //        ClaimsIdentity.DefaultRoleClaimType);
        //    return claimsIdentity;

        // //   return base.GetIdentity(username, rols);
        //}
    }
    #endregion
}
