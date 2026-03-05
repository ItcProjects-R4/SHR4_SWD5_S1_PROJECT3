namespace SimpleStoryMangementSystem.UI.UI.OrderUi;

public static class OrderDisplaymenu
{
    public static void DisplayMenu()
    {
        string choice;
        do
        {
            Console.Clear();
            ShowMenu();
            choice = Console.ReadLine() ?? "";
            ProcessChoice(choice);
        } while (choice != "5");
    }

    private static void ShowMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("         ORDER MANAGEMENT SYSTEM        ");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("1. Add Order");
        Console.WriteLine("2. Search Order By Id");
        Console.WriteLine("3. Delete Order");
        Console.WriteLine("4. Display All Orders");
        Console.WriteLine("5. Exit");
        Console.WriteLine();
        Console.Write("Enter your choice (1-5): ");
    }

    private static void ProcessChoice(string choice)
    {
        try
        {
            switch (choice)
            {
                case "1": OrderServicesUi.AddOrderConsole();    break;
                case "2": OrderServicesUi.SearchOrderConsole(); break;
                case "3": OrderServicesUi.DeleteOrderConsole(); break;
                case "4": OrderServicesUi.DisplayOrders();      break;
                case "5": Console.WriteLine("Goodbye!");        break;
                default:
                    Console.WriteLine("\nInvalid choice.");
                    OrderServicesUi.WaitForInput();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
            OrderServicesUi.WaitForInput();
        }
    }
}
