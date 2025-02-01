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

        public void Delete(Product productId)
        {
            
             _productDal.Delete(productId);
        }

        public void DeleteImage(Productİmage product)
        {
            _productImageDal.Delete(product);
        }

        public List<Product> GetAll()
        {
            return  _productDal.GetList().ToList(); 
        }

        public Product GetById(int id)
        {
            var product = _productDal.Get(p=> p.Id == id);
            return product;
        }

        public List<Product> GetProductByCategoryId(int productId)
        {
            return _productDal.GetList(p => p.CategoryId == productId).ToList();
        }

        public List<Productİmage> GetProductImages(int productId)
        {
            return _productImageDal.GetList(img => img.ProductId == productId).ToList();
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
