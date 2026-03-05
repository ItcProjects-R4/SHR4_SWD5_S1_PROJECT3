using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;

namespace SimpleStoryMangementSystem.Services.Implementations;

public class OrderDetailsServices : IOrderDetilsServices
{
    private readonly List<OrderDetails> _orderDetailsList;
    private int _nextId;

    List<OrderDetails> _seedData = new List<OrderDetails>(
    [
        new OrderDetails { id=1, orderId=1, productId=1, quantity=1, UnitPrice=15000m, SubTotal=15000m },
        new OrderDetails { id=2, orderId=1, productId=2, quantity=2, UnitPrice=250m,   SubTotal=500m   },
        new OrderDetails { id=3, orderId=2, productId=3, quantity=1, UnitPrice=450m,   SubTotal=450m   },
        new OrderDetails { id=4, orderId=2, productId=4, quantity=1, UnitPrice=5500m,  SubTotal=5500m  },
        new OrderDetails { id=5, orderId=2, productId=5, quantity=2, UnitPrice=1200m,  SubTotal=2400m  },
        new OrderDetails { id=6, orderId=3, productId=6, quantity=3, UnitPrice=350m,   SubTotal=1050m  }
    ]);

    public OrderDetailsServices()
    {
        _orderDetailsList = _seedData;
        _nextId = _orderDetailsList.Count + 1;
    }

    // ==================== ADD ====================
    public void AddOrderDetails(OrderDetails orderDetails)
    {
        if (orderDetails == null)
            throw new ArgumentNullException(nameof(orderDetails));

        orderDetails.id = _nextId++;
        orderDetails.SubTotal = CalculateSubTotal(orderDetails.UnitPrice, orderDetails.quantity);
        _orderDetailsList.Add(orderDetails);
    }

    // ==================== DELETE ====================
    public void DeleteOrderDetails(int id)
    {
        var item = _orderDetailsList.FirstOrDefault(od => od.id == id);
        if (item == null)
            throw new ArgumentException($"OrderDetails with ID {id} not found.");
        _orderDetailsList.Remove(item);
    }

    // ==================== UPDATE ====================
    public void UpdateOrderDetails(OrderDetails orderDetails)
    {
        var existing = _orderDetailsList.FirstOrDefault(od => od.id == orderDetails.id);
        if (existing == null)
            throw new ArgumentException($"OrderDetails with ID {orderDetails.id} not found.");

        existing.orderId   = orderDetails.orderId;
        existing.productId = orderDetails.productId;
        existing.quantity  = orderDetails.quantity;
        existing.UnitPrice = orderDetails.UnitPrice;
        existing.SubTotal  = CalculateSubTotal(existing.UnitPrice, existing.quantity);
    }

    // ==================== GET ALL ====================
    public List<OrderDetails> GetAllOrderDetails()
    {
        return new List<OrderDetails>(_orderDetailsList);
    }

    // ==================== SEARCH BY ID ====================
    public (bool, OrderDetails) SearchOrderDetailsById(int id)
    {
        var item = _orderDetailsList.FirstOrDefault(od => od.id == id);
        if (item != null)
            return (true, item);
        return (false, null!);
    }

    // ============================================================
    //              PRICE CALCULATION METHODS
    // ============================================================

    /// <summary>
    /// SubTotal لـ Item واحد = UnitPrice * Quantity
    /// </summary>
    public decimal CalculateSubTotal(decimal unitPrice, int quantity)
        => unitPrice * quantity;

    /// <summary>
    /// TotalAmount للاوردر كله = جمع كل المنتجات
    /// الـ Customer ممكن يطلب اكتر من Product في نفس الاوردر
    /// </summary>
    public decimal CalculateOrderTotal(int orderId)
    {
        var items = _orderDetailsList.Where(od => od.orderId == orderId).ToList();
        if (!items.Any()) return 0;
        return items.Sum(od => od.quantity * od.UnitPrice);
    }

    /// <summary>
    /// التوتال الكلي لكل الاوردرات بتاعة Customer معين
    /// </summary>
    public decimal CalculateTotalAmountForCustomer(int customerId, List<Order> allOrders)
    {
        var customerOrderIds = allOrders
            .Where(o => o.CustomerId == customerId)
            .Select(o => o.Id)
            .ToList();

        return _orderDetailsList
            .Where(od => customerOrderIds.Contains(od.orderId))
            .Sum(od => od.quantity * od.UnitPrice);
    }

    /// <summary>
    /// بيجيب كل المنتجات في اوردر معين
    /// </summary>
    public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        => _orderDetailsList.Where(od => od.orderId == orderId).ToList();

    /// <summary>
    /// بيحسب التوتال وبيحدث الـ Order object
    /// </summary>
    public decimal CalculateAndUpdateOrderTotal(Order order)
    {
        order.TotalAmount = CalculateOrderTotal(order.Id);
        return order.TotalAmount;
    }
}
