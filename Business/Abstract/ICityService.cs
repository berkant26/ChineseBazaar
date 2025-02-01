using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICityService
    {
        City GetCity(int id);
        List<City> GetCities();
    }
}
