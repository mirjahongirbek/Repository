using Entity;
using EntityRepository.Context;
using EntityRepository.Repository;
using RepositoryRule.Base;

namespace ServiceList
{
    public interface ICompanyService : IRepositoryBase<Company, int>
    {

    }
    public interface IProductService : IRepositoryBase<Product, int>
    {

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
    }
    #endregion
}
