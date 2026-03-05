using SimpleStoryMangementSystem.UI.UI.CustomerUi;
using SimpleStoryMangementSystem.UI.UI.OrderUi;
using SimpleStoryMangementSystem.UI.UI.ProductUi;
using SimpleStoryMangementSystem.UI.UI.OrderDetailsUi;

namespace SimpleStoryMangementSystem.UI;

public class Program
{
    public static void Main(string[] args)
    {
        string choice;
        do
        {
            Console.Clear();
            ShowMainMenu();
            choice = Console.ReadLine() ?? "";
            ProcessMainMenuChoice(choice);
        } while (choice != "5");
    }

    private static void ShowMainMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   SIMPLE STORY MANAGEMENT SYSTEM       ");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("1. Customer Management");
        Console.WriteLine("2. Order Management");
        Console.WriteLine("3. Product Management");
        Console.WriteLine("4. OrderDetails Management");
        Console.WriteLine("5. Exit");
        Console.WriteLine();
        Console.Write("Enter your choice (1-5): ");
    }

    private static void ProcessMainMenuChoice(string choice)
    {
        switch (choice)
        {
            case "1": CustomerDisplaymenu.DisplayMenu();     break;
            case "2": OrderDisplaymenu.DisplayMenu();        break;
            case "3": ProductDisplaymenu.DisplayMenu();      break;
            case "4": OrderDetailsDisplaymenu.DisplayMenu(); break;
            case "5": Console.WriteLine("Goodbye!"); break;
            default:
                Console.WriteLine("\nInvalid choice. Please enter a number from 1 to 5.");
                Console.ResetColor();
                break;
        }
    }
}
