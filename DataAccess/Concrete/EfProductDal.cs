using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EfProductDal : EfEntityRepositoryBase<Product,ChineseBazaarContext>,IProductDal
    {

    }
}
