using Entities.Models;

namespace Business.Abstract
{
    public interface IProductService
    {
        void Add(Product product);
        List<Product> GetAll();
    }
}
