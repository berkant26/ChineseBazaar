using Business.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductImagesManager : IProductImageService
    {
        

        List<Productİmage> IProductImageService.GetImagesByProductId(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
