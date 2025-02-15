using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        void Add(Order order);
        Order GetOrder(int userId);
        List<Order> GetAllUserOrder(int userId);
        List<Order> GetAllOrder();

    }
}
