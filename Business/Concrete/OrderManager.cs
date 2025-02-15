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
    public class OrderManager : IOrderService
    {
        public IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;               
        }
        public void Add(Order order)
        {
            _orderDal.Add(order);
        }

        // for admin
        public List<Order> GetAllOrder()
        {
            return _orderDal.GetList().ToList();
        }

        //for user
        public List<Order> GetAllUserOrder(int userId)
        {
            return _orderDal.GetList(o => o.UserId == userId).ToList();
        }

        public Order GetOrder(int userId)
        {
            return _orderDal.Get(o=> o.UserId == userId);
        }
    }
}
