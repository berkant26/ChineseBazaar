using Business.Abstract;
using Core.Helper;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private IProductImageService _productImages;
        

        public ProductsController(IProductService productService, IProductImageService productImages)
        {
            _productService = productService;
            _productImages = productImages;
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

        [Authorize(Roles = "Product.Add")]
        [HttpPost("add")]
        public IActionResult Add([FromBody] Product product)
        {
            _productService.Add(product);
            return Ok(new { productId = product.Id, message = "Product added successfully" });
        }
        //  [Authorize(Roles = "Product.Add")]
        [HttpPost("uploadImages")]
        public IActionResult UploadImages([FromForm] int productId, [FromForm] List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
            {
                return BadRequest($"Resimler boş. resimler listesindeki elemanlar :{images.Count}");
            }

            foreach (var image in images)
            {
                var uploadedImagePath = ImageUploader.UploadImage(image);
                if (!string.IsNullOrEmpty(uploadedImagePath))
                {
                    var productImage = new Productİmage
                    {
                        ProductId = productId,
                        ImageUrl = uploadedImagePath
                    };
                    _productImages.Add(productImage);
                }
                else
                {
                    return BadRequest("Image upload failed for one or more images.");
                }
            }

            return Ok("Images uploaded successfully");
        }



        [HttpPut("update/{productId}")]
        public IActionResult UpdateProduct(int productId, [FromBody] Product product)
        {
            var existingProduct = _productService.GetById(productId);
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockAmount = product.StockAmount; 

            _productService.Update(existingProduct);

            return Ok(new { productId = existingProduct.Id, message = "Product updated successfully" });
        }

        [HttpPut("updateImages/{productId}")]
        public IActionResult UpdateProductImages(int productId, [FromForm] List<IFormFile> images)
        {
            var existingProduct = _productService.GetById(productId);
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            // Mevcut resimleri sil (isteğe bağlı)
            var existingImages = _productImages.GetImagesByProductId(productId).ToList();
            foreach (var existingImage in existingImages)
            {
                _productService.DeleteImage(existingImage);
            }

            // Yeni resimleri yükle
            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    var uploadedImagePath = ImageUploader.UploadImage(image);
                    if (!string.IsNullOrEmpty(uploadedImagePath))
                    {
                        var productImage = new Productİmage
                        {
                            ProductId = productId,
                            ImageUrl = uploadedImagePath
                        };
                        _productImages.Add(productImage);
                    }
                    else
                    {
                        return BadRequest("Image upload failed for one or more images.");
                    }
                }
            }

            return Ok("Product images updated successfully");
        }
        [HttpPost("delete")]

        public IActionResult DeleteProduct(Product product)
        {
            _productService.Delete(product);
            return Ok("ürün silindi");
        }
        [HttpPost("deleteProductImage")]

        public IActionResult DeleteProductImage(List<Productİmage> productİmage)
        {
            foreach (var image in productİmage)
            {
                _productService.DeleteImage(image);


            }
            return Ok("ürün resmi silindi");
        }

    }
}
