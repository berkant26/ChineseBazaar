using Business.Abstract;
using Entities.Models;
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

        [HttpGet]
        public List<Category> GetCategories() { 
        
            return _categorySerivce.GetCategories();
        
        }
    }
}
