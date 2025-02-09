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
    public class OrderItemManager : IOrderItemService
    {
        IOrderItemsDal _orderItemsDal;
        public OrderItemManager(IOrderItemsDal orderItems)
        {
            _orderItemsDal = orderItems;
        }
        public void Add(OrderItem orderItem)
        {
           _orderItemsDal.Add(orderItem);
        }

        public List<OrderItem> GetOrderItems(int orderId)
        {
            return _orderItemsDal.GetList(o=> orderId == o.OrderId).ToList();
        }
    }
}
