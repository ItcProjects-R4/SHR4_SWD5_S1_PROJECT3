using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;

namespace SimpleStoryMangementSystem.Services.Implementations;

public class OrderServices : IOrderServices
{
    private readonly List<Order> _orders;
    private int _nextId;

    List<Order> _seedData = new List<Order>(
    [
        new Order { Id = 1, CustomerId = 1, OrderDate = new DateTime(2024, 1, 15), TotalAmount = 15500m },
        new Order { Id = 2, CustomerId = 2, OrderDate = new DateTime(2024, 1, 20), TotalAmount = 8350m  },
        new Order { Id = 3, CustomerId = 3, OrderDate = new DateTime(2024, 2, 1),  TotalAmount = 1050m  }
    ]);

    public OrderServices()
    {
        _orders = _seedData;
        _nextId = _orders.Count + 1;
    }

    public void AddOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        order.Id = _nextId++;
        order.OrderDate = DateTime.Now;
        _orders.Add(order);
    }

    public void DeleteOrder(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order == null) throw new ArgumentException($"Order with ID {id} not found.");
        _orders.Remove(order);
    }

    public void UpdateOrder(Order order)
    {
        var existing = _orders.FirstOrDefault(o => o.Id == order.Id);
        if (existing == null) throw new ArgumentException($"Order with ID {order.Id} not found.");
        existing.CustomerId  = order.CustomerId;
        existing.OrderDate   = order.OrderDate;
        existing.TotalAmount = order.TotalAmount;
    }

    public List<Order> GetAllOrders()
        => new List<Order>(_orders);

    public bool SearchOrderById(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            Console.WriteLine(order);
            return true;
        }
        return false;
    }

    public List<Order> GetOrdersByCustomerId(int customerId)
        => _orders.Where(o => o.CustomerId == customerId).ToList();
}
