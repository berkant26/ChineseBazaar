using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(Product productId);
        void DeleteImage(Productİmage product);

        Product GetById(int id);

        List<Product> GetAll();
        List<Product> GetProductByCategoryId(int categoryId);
        List<Productİmage> GetProductImages(int productId);
    }
}
