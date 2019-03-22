using Entity;

using RepositoryRule.Base;

namespace ServiceList
{

    public interface IDataService: IRepositoryBase<Data, string>
    {

    }
    public interface IEntityDataService:IRepositoryBase<EntityData, int>
    {

    }
   
   

}
