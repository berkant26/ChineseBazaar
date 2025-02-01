using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INeighborhoodService
    {
        Neighborhood GetNeighborhood(int id);
        List<Neighborhood> GetNeighborhoods(int districtId);
    }
}
