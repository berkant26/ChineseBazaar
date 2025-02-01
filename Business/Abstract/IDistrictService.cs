using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDistrictService
    {
        District GetDistrict(int id);
        List<District> GetDistricts(int cityId);
    }
}
