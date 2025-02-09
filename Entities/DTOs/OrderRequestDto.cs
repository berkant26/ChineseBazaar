using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OrderRequestDto : IDto
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
