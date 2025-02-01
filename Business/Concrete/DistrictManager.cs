using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class DistrictManager : IDistrictService
    {
        IDistrictDal _districtDal;
        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }

        

        public District GetDistrict(int id)
        {
            throw new NotImplementedException();
        }

        public List<District> GetDistricts(int cityId)
        {
            // Get all districts from the data layer
            var districts = _districtDal.GetList()
                .Where(d => d.CityId == cityId) // Filter by cityId
                .ToList(); // Materialize the query into a list

            return districts;
        }

    }

}
