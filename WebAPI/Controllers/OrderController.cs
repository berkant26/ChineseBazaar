using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _orderService;
        public IOrderItemService _orderItemService;
        private IProductService _productService;


        public OrderController(IOrderService orderService, IOrderItemService orderItemService, IProductService productService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _productService = productService;   
        }


        [HttpGet("getUserOrders")]
        public IActionResult Get(int userId)
        {
            // Siparişi al
            var order = _orderService.GetOrder(userId);

            if (order == null)
            {
                return BadRequest(new { message = "Sipariş bulunamadı" });
            }

            var orderItems = _orderItemService.GetOrderItems(order.Id);

            // Kullanıcı adresini al

            var productNames = _productService.GetAll();

            return Ok(new
            {
                Order = new
                {
                    order.Id,
                    order.OrderDate,
                    TotalPrice = order.TotalPrice // Hesaplanan toplam tutar
                },
                OrderItems = orderItems.Select(o => new
                {
                    ProductId = o.ProductId,
                    ProductName = productNames.FirstOrDefault(p => p.Id == o.ProductId)?.Name, // Ürün adı
                    o.Quantity, // Adet
                    o.Price // Fiyat
                }).ToList()
            });
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
