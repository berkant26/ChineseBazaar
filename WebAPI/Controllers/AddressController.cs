using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        ChineseBazaarContext _context = new ChineseBazaarContext();
        ICityService _cityService;
        IDistrictService _districtService;
        INeighborhoodService _neighborhoodService;
        public AddressController(ICityService cityService, IDistrictService districtService,INeighborhoodService neighborhoodService)
        {
            _cityService = cityService;
            _districtService = districtService;
            _neighborhoodService = neighborhoodService;
        }

        [HttpGet("getCities")]
        public List<City> GetCities()
        {
            return _cityService.GetCities();
            
        }
        [HttpGet("districts/{cityId}")]
        public List<District> GetDistricts(int cityId)
        {
            return _districtService.GetDistricts( cityId);

        }
        [HttpGet("neighborhoods/{districtId}")]
        public List<Neighborhood> GetNeighborhoods(int districtId)
        {
            return _neighborhoodService.GetNeighborhoods(districtId);

        }

        [HttpPost("saveUserAddress")]
        public IActionResult SaveUserAddress([FromBody] Address userAddress)
        {
            // if adress is exist update register  otherwise  add new register
            if (userAddress == null)
            {
                return BadRequest("Invalid user address data.");
            }

            _context.Addresses.Add(userAddress);
            _context.SaveChanges();
            return Ok(new { message = "Address saved successfully" });
        }
        [HttpGet("getUserAddress/{userId}")]
        public IActionResult GetUserAddress(int userId)
        {

            var address = _context.Addresses.FirstOrDefault(a => a.UserId == userId);
            var _cityName = _context.Cities
    .FirstOrDefault(p => p.Id == address.CityId)?.Name ?? "Unknown City";
            var _districtName = _context.Districts
    .FirstOrDefault(p => p.Id == address.DistrictId)?.Name ?? "Unknown District";
            var _neighborhood = _context.Neighborhoods
    .FirstOrDefault(p => p.Id == address.NeighborhoodId)?.Name ?? "Unknown Neighborhood";
            if (address == null)
                return NotFound("Address not found.");

            var convertedAdress = new
            {
                firstName = address.FirstName,
                lastName = address.LastName,
                phoneNumber = address.PhoneNumber,
                addressDescription = address.AddressDescription,
                cityName = _cityName,
                districtName = _districtName,
                neighborhoodName = _neighborhood
            };

            return Ok(convertedAdress);
        }

        [HttpPut("updateUserAddress")]
        public IActionResult UpdateUserAddress([FromBody] Address userAddressDto)
        {
            if (userAddressDto == null)
                return BadRequest("Invalid address data.");

            var existingAddress = _context.Addresses.FirstOrDefault(a => a.UserId == userAddressDto.UserId);
            if (existingAddress == null)
                return NotFound("Address not found.");

            // Update the existing address
            existingAddress.FirstName = userAddressDto.FirstName;
            existingAddress.LastName = userAddressDto.LastName;
            existingAddress.PhoneNumber = userAddressDto.PhoneNumber;
            existingAddress.AddressDescription = userAddressDto.AddressDescription;
            existingAddress.CityId = userAddressDto.CityId;
            existingAddress.DistrictId = userAddressDto.DistrictId;
            existingAddress.NeighborhoodId = userAddressDto.NeighborhoodId;

            _context.SaveChanges();

            return Ok("Address updated successfully.");
        }


    }
}
