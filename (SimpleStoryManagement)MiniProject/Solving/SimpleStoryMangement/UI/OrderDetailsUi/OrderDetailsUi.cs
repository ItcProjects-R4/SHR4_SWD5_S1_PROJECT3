using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Implementations;

namespace SimpleStoryMangementSystem.UI.UI.OrderDetailsUi;

public class OrderDetailsUi
{
    public static OrderDetailsServices _service = new OrderDetailsServices();

    public static void WaitForInput()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    public static void AddOrderDetailsConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Add Product to Order ---");
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }
        Console.Write("Enter Quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int qty)) { Console.WriteLine("Invalid quantity."); WaitForInput(); return; }
        Console.Write("Enter Unit Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price)) { Console.WriteLine("Invalid price."); WaitForInput(); return; }

        _service.AddOrderDetails(new OrderDetails
        {
            orderId   = orderId,
            productId = productId,
            quantity  = qty,
            UnitPrice = price
        });

        Console.WriteLine("\nProduct added to order successfully.");
        WaitForInput();
    }

    public static void DeleteOrderDetailsConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Delete OrderDetail ---");
        DisplayOrderDetailsTable(_service.GetAllOrderDetails());
        Console.Write("\nEnter OrderDetail ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }
        _service.DeleteOrderDetails(id);
        Console.WriteLine("\nDeleted successfully.");
        WaitForInput();
    }

    public static void DisplayOrderDetails()
    {
        Console.Clear();
        Console.WriteLine("--- All Order Details ---");
        var list = _service.GetAllOrderDetails();
        if (list.Count == 0) { Console.WriteLine("No order details found."); WaitForInput(); return; }
        DisplayOrderDetailsTable(list);
        Console.WriteLine($"\nTotal: {list.Count} item(s)");
        WaitForInput();
    }

    public static void CalculateOrderTotalConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Calculate Order Total ---");
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }

        var items = _service.GetOrderDetailsByOrderId(orderId);
        if (items.Count == 0)
        {
            Console.WriteLine($"No items found for Order ID {orderId}.");
            WaitForInput(); return;
        }

        Console.WriteLine();
        Console.WriteLine($"{"Product ID",-12} | {"Quantity",-10} | {"UnitPrice",-12} | {"SubTotal",-12}");
        Console.WriteLine("--------------------------------------------------");
        foreach (var item in items)
            Console.WriteLine($"{item.productId,-12} | {item.quantity,-10} | {item.UnitPrice,-12:C} | {item.SubTotal,-12:C}");

        decimal total = _service.CalculateOrderTotal(orderId);
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"{"TOTAL",-12}   {"":10}   {"":12}   {total:C}");
        WaitForInput();
    }

    public static void CalculateCustomerTotalConsole(List<Order> allOrders)
    {
        Console.Clear();
        Console.WriteLine("--- Calculate Total Amount for Customer ---");
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }

        decimal total = _service.CalculateTotalAmountForCustomer(customerId, allOrders);
        Console.WriteLine($"\nTotal amount spent by Customer ID {customerId}: {total:C}");
        WaitForInput();
    }

    public static void DisplayOrderDetailsTable(List<OrderDetails> list)
    {
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine($"{"ID",-5} | {"OrderId",-8} | {"ProductId",-10} | {"Qty",-5} | {"UnitPrice",-12} | {"SubTotal",-12}");
        Console.WriteLine("-------------------------------------------------------------------------");
        foreach (var od in list)
            Console.WriteLine($"{od.id,-5} | {od.orderId,-8} | {od.productId,-10} | {od.quantity,-5} | {od.UnitPrice,-12} | {od.SubTotal,-12}");
        Console.WriteLine("-------------------------------------------------------------------------");
    }
}
