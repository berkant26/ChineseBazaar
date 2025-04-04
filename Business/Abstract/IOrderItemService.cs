﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderItemService
    {
        void Add(OrderItem orderItem);
         List<OrderItem> GetAllUserItems(int orderId);
        List<OrderItem> GetAllOrderItems();

    }
}
