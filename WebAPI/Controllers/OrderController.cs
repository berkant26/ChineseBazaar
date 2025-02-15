using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Iyzipay.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _orderService;
        public IOrderItemService _orderItemService;
        private IProductService _productService;
        ChineseBazaarContext _context = new ChineseBazaarContext();
        ICityService _cityService;
        IDistrictService _districtService;
        INeighborhoodService _neighborhoodService;
        public OrderController(IOrderService orderService, IOrderItemService orderItemService, IProductService productService, ICityService cityService, IDistrictService districtService, INeighborhoodService neighborhoodService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _productService = productService;
            _cityService = cityService;
            _districtService = districtService;
            _neighborhoodService = neighborhoodService;
        }
        [HttpGet("getAllOrders")]
        
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = _orderService.GetAllOrder();

            if (orders == null || !orders.Any())
            {
                return BadRequest(new { message = "No orders found." });
            }

            var productNames = _productService.GetAll();

            // Prepare the response list
            var response = new List<object>();

            foreach (var order in orders)
            {
                // Fetch all items **only for this order**
                var orderItems = _orderItemService.GetAllOrderItems()
                    .Where(o => o.OrderId == order.Id)
                    .ToList();

                // Fetch user address **before constructing the response**
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.UserId == order.UserId);

                string cityName = "Unknown City";
                string districtName = "Unknown District";
                string neighborhood = "Unknown Neighborhood";
                string firstName = address.FirstName;
                string lastName = address.LastName;
                string phoneNumber = address.PhoneNumber;
                string fullAddress = address.AddressDescription;

                if (address != null)
                {
                    cityName = await _context.Cities
                        .Where(c => c.Id == address.CityId)
                        .Select(c => c.Name)
                        .FirstOrDefaultAsync() ?? "Unknown City";

                    districtName = await _context.Districts
                        .Where(d => d.Id == address.DistrictId)
                        .Select(d => d.Name)
                        .FirstOrDefaultAsync() ?? "Unknown District";

                    neighborhood = await _context.Neighborhoods
                        .Where(n => n.Id == address.NeighborhoodId)
                        .Select(n => n.Name)
                        .FirstOrDefaultAsync() ?? "Unknown Neighborhood";

                   
                }

                // Construct the order response
                var orderResponse = new
                {
                    Order = new
                    {
                        order.Id,
                        order.OrderDate,
                        TotalPrice = order.TotalPrice
                    },
                    OrderItems = orderItems.Select(o => new
                    {
                        ProductId = o.ProductId,
                        ProductName = productNames.FirstOrDefault(p => p.Id == o.ProductId)?.Name,
                        o.Quantity,
                        o.Price,
                        ProductDescription = productNames.FirstOrDefault(p => p.Id == o.ProductId)?.Description
                    }).ToList(),
                    UserAddress = new
                    {
                        City = cityName,
                        District = districtName,
                        Neighborhood = neighborhood,
                        PhoneNumber= phoneNumber,
                        FirstName = firstName,
                        LastName = lastName,
                        FullAddress = fullAddress

                    }
                };

                response.Add(orderResponse);
            }

            return Ok(response);
        }









        [HttpGet("getUserOrders")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            // Fetch all orders for the user
            var orders = _orderService.GetAllUserOrder(userId);

            if (orders == null || !orders.Any())
            {
                return BadRequest(new { message = "No orders found." });
            }

            var productNames = _productService.GetAll();

            var response = new List<object>();

            foreach (var order in orders)
            {
                // Fetch all items for the current order
                var orderItems = _orderItemService.GetAllUserItems(order.Id);

                // Fetch user address
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.UserId == userId);

                string cityName = "Unknown City";
                string districtName = "Unknown District";
                string neighborhood = "Unknown Neighborhood";
                string firstName = address?.FirstName ?? "N/A";
                string lastName = address?.LastName ?? "N/A";
                string phoneNumber = address?.PhoneNumber ?? "N/A";
                string fullAddress = address.AddressDescription;
                if (address != null)
                {
                    cityName = await _context.Cities
                        .Where(c => c.Id == address.CityId)
                        .Select(c => c.Name)
                        .FirstOrDefaultAsync() ?? "Unknown City";

                    districtName = await _context.Districts
                        .Where(d => d.Id == address.DistrictId)
                        .Select(d => d.Name)
                        .FirstOrDefaultAsync() ?? "Unknown District";

                    neighborhood = await _context.Neighborhoods
                        .Where(n => n.Id == address.NeighborhoodId)
                        .Select(n => n.Name)
                        .FirstOrDefaultAsync() ?? "Unknown Neighborhood";
                }

                // Construct the order response
                var orderResponse = new
                {
                    Order = new
                    {
                        order.Id,
                        order.OrderDate,
                        TotalPrice = order.TotalPrice
                    },
                    OrderItems = orderItems.Select(o => new
                    {
                        ProductId = o.ProductId,
                        ProductName = productNames.FirstOrDefault(p => p.Id == o.ProductId)?.Name,
                        o.Quantity,
                        o.Price,
                        ProductDescription = productNames.FirstOrDefault(p => p.Id == o.ProductId)?.Description
                    }).ToList(),
                    UserAddress = new
                    {
                        City = cityName,
                        District = districtName,
                        Neighborhood = neighborhood,
                        PhoneNumber = phoneNumber,
                        FirstName = firstName,
                        LastName = lastName,
                        FullAddress = fullAddress
                    }
                };

                response.Add(orderResponse);
            }

            return Ok(response);
        }




        [HttpPost("addUserOrder")]
        public IActionResult AddOrder([FromBody] OrderRequestDto request)
        {
            if (request == null || request.Order == null || request.OrderItems == null || request.OrderItems.Count == 0)
            {
                return BadRequest(new { message = "Invalid order data." });
            }

            request.Order.OrderDate = DateTime.UtcNow;
            request.Order.PaymentStatus = true; // Kept as bool
            request.Order.UserAddressId = request.Order.UserId;
            _orderService.Add(request.Order);

            foreach (var item in request.OrderItems)
            {
                item.OrderId = request.Order.Id; // Assign OrderId after saving
                _orderItemService.Add(item);
            }

            return Ok(new { message = "Order created successfully." });
        }

    }
}
