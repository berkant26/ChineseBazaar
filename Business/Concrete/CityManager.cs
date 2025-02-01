using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CityManager : ICityService
    {
        ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public List<City> GetCities()
        {
            var cities = _cityDal.GetList();

            cities.Select(c => new { c.Id, c.Name });
           
            return cities.ToList();
        }

        public City GetCity(int id)
        {
            throw new NotImplementedException();
        }
    }

}
