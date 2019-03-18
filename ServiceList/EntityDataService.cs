using System.Threading.Tasks;
using Entity;
using EntityRepository;
using EntityRepository.Context;
using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace ServiceList
{
    public class EntityDataService : SqlRepository<EntityData>, IEntityDataService
    {
        public EntityDataService(IDataContext context) : base(context)
        {
        }
    }
    public class ExampleCommand : IControllerCommand<EntityData, int>
    {
        public string Name { get { return "Example"; } }

        public Task<ICommandResult> Add(EntityData model, IAuthEntiy<int> user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResult> Delete(EntityData model, IAuthEntiy<int> user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResult> Read(EntityData model, IAuthEntiy<int> user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResult> Update(EntityData model, IAuthEntiy<int> user)
        {
            throw new System.NotImplementedException();
        }
    }
}
