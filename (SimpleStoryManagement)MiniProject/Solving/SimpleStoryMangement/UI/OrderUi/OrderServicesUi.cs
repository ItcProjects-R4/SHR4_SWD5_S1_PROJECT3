using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;
using SimpleStoryMangementSystem.Services.Implementations;

namespace SimpleStoryMangementSystem.UI.UI.OrderUi;

public class OrderServicesUi
{
    public static IOrderServices _service = new OrderServices();

    public static void WaitForInput()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    public static void AddOrderConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Add New Order ---");
        Console.WriteLine();

        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Error: Invalid Customer ID.");
            WaitForInput(); return;
        }

        _service.AddOrder(new Order
        {
            CustomerId  = customerId,
            OrderDate   = DateTime.Now,
            TotalAmount = 0
        });

        Console.WriteLine("\nOrder added successfully.");
        WaitForInput();
    }

    public static void DeleteOrderConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Delete Order ---");
        DisplayOrdersTable(_service.GetAllOrders());

        Console.Write("\nEnter Order ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Error: Invalid ID."); WaitForInput(); return;
        }

        _service.DeleteOrder(id);
        Console.WriteLine("\nOrder deleted successfully.");
        WaitForInput();
    }

    public static void DisplayOrders()
    {
        Console.Clear();
        Console.WriteLine("--- All Orders ---");
        var orders = _service.GetAllOrders();
        if (orders.Count == 0) { Console.WriteLine("No orders found."); WaitForInput(); return; }
        DisplayOrdersTable(orders);
        Console.WriteLine($"\nTotal: {orders.Count} order(s)");
        WaitForInput();
    }

    public static void SearchOrderConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Search Order By ID ---");
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Error: Invalid ID."); WaitForInput(); return;
        }

        bool found = _service.SearchOrderById(id);
        if (!found) Console.WriteLine($"Order with ID {id} not found.");
        WaitForInput();
    }

    public static void DisplayOrdersTable(List<Order> orders)
    {
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine($"{"ID",-5} | {"CustomerID",-12} | {"OrderDate",-15} | {"TotalAmount",-15}");
        Console.WriteLine("--------------------------------------------------------------");
        foreach (var o in orders)
            Console.WriteLine($"{o.Id,-5} | {o.CustomerId,-12} | {o.OrderDate:d,-15} | {o.TotalAmount,-15}");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
