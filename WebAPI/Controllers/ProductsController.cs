using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public List<Product> GetAllProducts()
        {
            return _productService.GetAll();
        }
        [HttpGet("getlistbycategoryId")]
        public List<Product> GetProductByCategory(int categoryId)
        {
            return _productService.GetProductByCategoryId(categoryId);

        }
        [HttpGet("getProductImages")]
        public List<Productİmage> GetProductImages(int productId)
        {
            return _productService.GetProductImages(productId);

        }
    }
}
