using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        IProductImageDal _productImageDal;
        public ProductManager(IProductDal productDal,IProductImageDal productImageDal)
        {
                _productDal = productDal;
                _productImageDal = productImageDal;
        }
        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public List<Product> GetAll()
        {
            return  _productDal.GetList().ToList(); 
        }

        public List<Product> GetProductByCategoryId(int productId)
        {
            return _productDal.GetList(p => p.CategoryId == productId).ToList();
        }

        public List<Productİmage> GetProductImages(int productId)
        {
            return _productImageDal.GetList(img => img.ProductId == productId).ToList();
        }
    }
}
