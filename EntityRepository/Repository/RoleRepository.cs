using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace EntityRepository.Repository
{
    public class RoleRepository<T> : IRoleRepository<T, int>
        where T : class, IRoleUser<int>
    {
        public void Add(T model)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T model)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T model)
        {
            throw new System.NotImplementedException();
        }
    }
}
