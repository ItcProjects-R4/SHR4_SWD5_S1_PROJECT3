using SimpleStoryMangementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Services.Interfaces;

public interface IOrderDetilsServices
{
        void AddOrderDetails(OrderDetails orderDetails);
        void DeleteOrderDetails(int Id);
        void UpdateOrderDetails(OrderDetails orderDetails);
    
        List<OrderDetails> GetAllOrderDetails();

        (bool, OrderDetails) SearchOrderDetailsById(int id);
}
