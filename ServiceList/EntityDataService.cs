using System.Security.Claims;
using System.Threading.Tasks;
using Entity;
using EntityRepository;
using EntityRepository.Context;
using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace ServiceList
{
  //  public class EntityDataService : SqlRepository<EntityData>, IEntityDataService
  //  {
  //      public EntityDataService(IDataContext context) : base(context)
  //      {
  //      }
  //  }
    public class ExampleCommand : IControllerCommand<int>
    {
        public string Name { get { return "Example"; } }

        public Task<ICommandResult> Add<T>(T model, ClaimsPrincipal user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResult> Delete<T>(T model, ClaimsPrincipal user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> GetById(int id, ClaimsPrincipal user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResult> Read<T>(T model, ClaimsPrincipal user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResult> Update<T>(T model, ClaimsPrincipal user)
        {
            throw new System.NotImplementedException();
        }
    }
}
