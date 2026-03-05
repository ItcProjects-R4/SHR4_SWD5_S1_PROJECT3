using SimpleStoryMangementSystem.Services.Implementations;

namespace SimpleStoryMangementSystem.UI.UI.OrderDetailsUi;

public static class OrderDetailsDisplaymenu
{
    public static void DisplayMenu()
    {
        var orderService = new OrderServices();

        string choice;
        do
        {
            Console.Clear();
            ShowMenu();
            choice = Console.ReadLine() ?? "";
            ProcessChoice(choice, orderService);
        } while (choice != "7");
    }

    private static void ShowMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("     ORDER DETAILS MANAGEMENT SYSTEM    ");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("1. Add Product to Order");
        Console.WriteLine("2. Delete Order Detail");
        Console.WriteLine("3. Display All Order Details");
        Console.WriteLine("4. Calculate Order Total (by Order ID)");
        Console.WriteLine("5. Calculate Total Spent by Customer");
        Console.WriteLine("6. Search Order Detail By ID");
        Console.WriteLine("7. Exit");
        Console.WriteLine();
        Console.Write("Enter your choice (1-7): ");
    }

    private static void ProcessChoice(string choice, OrderServices orderService)
    {
        try
        {
            switch (choice)
            {
                case "1": OrderDetailsUi.AddOrderDetailsConsole();    break;
                case "2": OrderDetailsUi.DeleteOrderDetailsConsole(); break;
                case "3": OrderDetailsUi.DisplayOrderDetails();       break;
                case "4": OrderDetailsUi.CalculateOrderTotalConsole(); break;
                case "5": OrderDetailsUi.CalculateCustomerTotalConsole(orderService.GetAllOrders()); break;
                case "6": SearchOrderDetailConsole();                 break;
                case "7": Console.WriteLine("Goodbye!");              break;
                default:
                    Console.WriteLine("\nInvalid choice.");
                    OrderDetailsUi.WaitForInput();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
            OrderDetailsUi.WaitForInput();
        }
    }

    private static void SearchOrderDetailConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Search OrderDetail By ID ---");
        Console.Write("Enter ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); OrderDetailsUi.WaitForInput(); return; }
        var (found, item) = OrderDetailsUi._service.SearchOrderDetailsById(id);
        if (found) Console.WriteLine(item);
        else Console.WriteLine($"OrderDetail with ID {id} not found.");
        OrderDetailsUi.WaitForInput();
    }
}
