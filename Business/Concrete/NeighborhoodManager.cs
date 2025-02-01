using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class NeighborhoodManager : INeighborhoodService
    {
        INeighborhoodDal _neighborhoodDal;
        public NeighborhoodManager(INeighborhoodDal neighborhoodDal)
        {
            _neighborhoodDal = neighborhoodDal;
        }

        public Neighborhood GetNeighborhood(int id)
        {
            throw new NotImplementedException();
        }

        public List<Neighborhood> GetNeighborhoods(int districtId)
        {
            var districts = _neighborhoodDal.GetList()
    .Where(d => d.DistrictId == districtId)
    .Select(d => new Neighborhood
    {
        Id = d.Id,
        Name = d.Name.Trim() 
    })
    .ToList(); 

            return districts;
        }
    }

}
