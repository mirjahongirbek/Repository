using Entity;
using EntityRepository.Context;
using RepositoryRule.Base;

namespace ServiceList
{
   public interface ICompanyService: IRepositoryBase<Company, int>
    {

    }
    public interface IProductService:IRepositoryBase<Product, int>
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
}
