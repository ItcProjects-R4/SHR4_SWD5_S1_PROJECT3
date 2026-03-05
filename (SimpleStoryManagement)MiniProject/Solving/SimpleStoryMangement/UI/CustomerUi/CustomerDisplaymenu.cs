using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services;

namespace SimpleStoryMangementSystem.UI.UI.CustomerUi;

public static class CustomerDisplaymenu
{
    public static void DisplayMenu()
    {
       CustomerServicesUi._service= new Services.Implementations.CustomerService();
        string choice;
        do
        {
            Console.Clear();
            ShowMenu();
            choice = Console.ReadLine();
            ProcessChoice(choice);
        } while (choice != "6");
    }
    private static void ShowMenu()
    {
      
        Console.WriteLine("========================================");
        Console.WriteLine("       CUSTOMER MANAGEMENT SYSTEM       ");
        Console.WriteLine("========================================");
       
        Console.WriteLine();
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Search Customer By Id");
        Console.WriteLine("3. Delete Customer");
        Console.WriteLine("4. Edit Customer");
        Console.WriteLine("5. Display All Customers");
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
                case "1":
                    CustomerServicesUi.AddCustomerConsole();
                    break;
                case "2":
                    CustomerServicesUi.SearchCustomerByIdConsole();
                    break;
                case "3":
                    CustomerServicesUi.DeleteCustomerConsole();
                    break;
                case "4":
                    CustomerServicesUi.UpdateCustomerConsole();
                    break;
                case "5":
                    CustomerServicesUi.DisplayCustomers();
                    break;
                case "6":   
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Please enter a number from 1 to 6.");
                    Console.ResetColor();
                    CustomerServicesUi.WaitForInput();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
            CustomerServicesUi.WaitForInput();
        }
    }
}

