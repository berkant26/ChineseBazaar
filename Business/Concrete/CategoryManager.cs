using Business.Abstract;
using DataAccess.Abstract;
using Entities.Models;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
                _categoryDal = categoryDal;
        }
        public List<Category> GetCategories()
        {
            return _categoryDal.GetList().ToList();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
