using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductImagesManager : IProductImageService
    {
        IProductImageDal _productImageDal;
        public ProductImagesManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }
        public List<Productİmage> GetImagesByProductId(int productId)
        {
            return _productImageDal.GetList().ToList();
        }
        public void Add(Productİmage productİmage)
        {
            _productImageDal.Add(productİmage);
        }
    }
}
