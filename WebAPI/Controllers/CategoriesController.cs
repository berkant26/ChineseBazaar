using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categorySerivce;
        public CategoriesController(ICategoryService categoryService)
        {
            _categorySerivce = categoryService;
        }

        [HttpGet("getAll")]

        public List<Category> GetAllCategories() { 
        
            return _categorySerivce.GetCategories();
        
        }
    }
}
