using SimpleStoryMangementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Services.Interfaces;

public interface IOrderServices
{
    void AddOrder(Order order);
    void DeleteOrder(int Id);
    void UpdateOrder(Order order);

    List<Order> GetAllOrders();

    bool SearchOrderById(int id);

}
