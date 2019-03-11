using Entity;
using MongoRepository.Context;

namespace ServiceList
{
    public class SelectDataService : MongoRepository.MongoRepository<SelectData>, ISelectDataService
    {
        public SelectDataService(IMongoContext context):base(context)
        {

        }

    }

}
