namespace SimpleStoryMangementSystem.UI.UI.ProductUi;

public static class ProductDisplaymenu
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
        } while (choice != "6");
    }

    private static void ShowMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("        PRODUCT MANAGEMENT SYSTEM       ");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. Search Product By Id");
        Console.WriteLine("3. Delete Product");
        Console.WriteLine("4. Edit Product");
        Console.WriteLine("5. Display All Products");
        Console.WriteLine("6. Exit");
        Console.WriteLine();
        Console.Write("Enter your choice (1-6): ");
    }

    private static void ProcessChoice(string choice)
    {
        try
        {
            switch (choice)
            {
                case "1": ProductServicesUi.AddProductConsole();    break;
                case "2": ProductServicesUi.SearchProductConsole(); break;
                case "3": ProductServicesUi.DeleteProductConsole(); break;
                case "4": ProductServicesUi.UpdateProductConsole(); break;
                case "5": ProductServicesUi.DisplayProducts();      break;
                case "6": Console.WriteLine("Goodbye!");            break;
                default:
                    Console.WriteLine("\nInvalid choice.");
                    ProductServicesUi.WaitForInput();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
            ProductServicesUi.WaitForInput();
        }
    }
}
