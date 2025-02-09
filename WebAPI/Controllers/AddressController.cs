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
        public async Task<List<Neighborhood>> GetNeighborhoods(int districtId)
        {
            if (districtId <= 0)
                throw new ArgumentException("District ID must be a positive number", nameof(districtId));

            var neighborhoods =  _neighborhoodService.GetNeighborhoods(districtId);

            if (neighborhoods == null || !neighborhoods.Any())
                throw new Exception("No neighborhoods found for the given district.");

            return neighborhoods;
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
        public async Task<IActionResult> GetUserAddressAsync(int userId)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);
            if (address == null)
            {
                return NotFound("Address not found.");
            }

            // Şehir, ilçe, mahalle adlarını güvenli bir şekilde almak
            var _cityName = await _context.Cities
                .Where(p => p.Id == address.CityId)
                .Select(p => p.Name)
                .FirstOrDefaultAsync() ?? "Unknown City";

            var _districtName = await _context.Districts
                .Where(p => p.Id == address.DistrictId)
                .Select(p => p.Name)
                .FirstOrDefaultAsync() ?? "Unknown District";

            var _neighborhood = await _context.Neighborhoods
                .Where(p => p.Id == address.NeighborhoodId)
                .Select(p => p.Name)
                .FirstOrDefaultAsync() ?? "Unknown Neighborhood";

            var convertedAddress = new
            {
                firstName = address.FirstName ?? "Unknown First Name", // Null kontrolü ekledik
                lastName = address.LastName ?? "Unknown Last Name",   // Null kontrolü ekledik
                phoneNumber = address.PhoneNumber ?? "Unknown Phone", // Null kontrolü ekledik
                addressDescription = address.AddressDescription ?? "No Description", // Null kontrolü ekledik
                cityName = _cityName,
                districtName = _districtName,
                neighborhoodName = _neighborhood
            };

            return Ok(convertedAddress);
        }


        [HttpPut("updateUserAddress")]
        public async Task<IActionResult> UpdateUserAddress([FromBody] Address userAddressDto)
        {
            if (userAddressDto == null)
                return BadRequest("Invalid address data.");

            var existingAddress = await _context.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userAddressDto.UserId);
            if (existingAddress == null)
                return NotFound("Address not found.");

            // Update the existing address with null checks
            existingAddress.FirstName = userAddressDto.FirstName ?? existingAddress.FirstName;
            existingAddress.LastName = userAddressDto.LastName ?? existingAddress.LastName;
            existingAddress.PhoneNumber = userAddressDto.PhoneNumber ?? existingAddress.PhoneNumber;
            existingAddress.AddressDescription = userAddressDto.AddressDescription ?? existingAddress.AddressDescription;
            existingAddress.CityId = userAddressDto.CityId != 0 ? userAddressDto.CityId : existingAddress.CityId;
            existingAddress.DistrictId = userAddressDto.DistrictId != 0 ? userAddressDto.DistrictId : existingAddress.DistrictId;
            existingAddress.NeighborhoodId = userAddressDto.NeighborhoodId != 0 ? userAddressDto.NeighborhoodId : existingAddress.NeighborhoodId;

            await _context.SaveChangesAsync();

            return Ok("Address updated successfully.");
        }



    }
}
