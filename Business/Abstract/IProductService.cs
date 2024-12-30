using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        void Add(Product product);
        List<Product> GetAll();
        List<Product> GetProductByCategoryId(int categoryId);
        List<Productİmage> GetProductImages(int productId);
    }
}
