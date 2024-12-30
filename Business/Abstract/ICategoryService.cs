using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Category GetCategory(int id);
        List<Category> GetCategories();
    }
}
