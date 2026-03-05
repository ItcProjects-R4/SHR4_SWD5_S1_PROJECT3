using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;
using System;
using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;
using SimpleStoryMangementSystem.Services.Implementations;
namespace SimpleStoryMangementSystem.UI.UI.CustomerUi;

public  class CustomerServicesUi
{
    public static ICustomerService _service;
  
    
    public static void WaitForInput()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
    public static void AddCustomerConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Add New Employee ---");
        Console.WriteLine();

        Console.Write("Enter FullName: ");
        string? Fullname = Console.ReadLine();

        Console.Write("Enter  Phone: ");
        string?  phone = Console.ReadLine();

        Console.Write("Enter Address: ");
        string? address = Console.ReadLine();
        Console.Write("Enter Email: ");
        string? email = Console.ReadLine();

        _service.AddCustomer(new Customer
        {
          FullName= Fullname,
          Phone = phone,
          Address = address,
          Email = email
        });

      
        Console.WriteLine("\nCustomer added successfully.");
        Console.ResetColor();
        WaitForInput();
    }
    public static void DeleteCustomerConsole()
    {
        Console.Clear();
        Console.WriteLine("-----------------------");
        Console.WriteLine("--- Delete Employee ---");
        Console.WriteLine("-----------------------");
        Console.WriteLine();

        var customers = _service.GetAllCustomers();
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            WaitForInput();
            return;
        }

        DisplayCustomersTable(customers);

        Console.Write("\nEnter Customer ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: Invalid ID format.");
            Console.ResetColor();
            WaitForInput();
            return;
        }

        _service.DeleteCustomer(id);
        Console.WriteLine("\nCustomer deleted successfully.");
        WaitForInput();
    }
    public static void UpdateCustomerConsole()
    {
        Console.Clear();
        Console.WriteLine("---------------------");
        Console.WriteLine("--- Edit Customer ---");
        Console.WriteLine("---------------------");
        Console.WriteLine();

        var customers = _service.GetAllCustomers();
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            WaitForInput();
            return;
        }

        DisplayCustomersTable(customers);

        Console.Write("\nEnter Customer ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: Invalid ID format.");
            Console.ResetColor();
            WaitForInput();
            return;
        }

        var existingCustomer = _service.GetAllCustomers().FirstOrDefault(c => c.Id == id);
        
        if (existingCustomer == null)
        {
            Console.WriteLine($"Error: Customer with ID {id} not found.");
            WaitForInput();
            return;
        }
        else 
        Console.WriteLine($"\nCurrent Data for Customer ID {id}:");
        Console.WriteLine($"  FullName: {existingCustomer.FullName}");
        Console.WriteLine($"  Phone: {existingCustomer.Phone}");
        Console.WriteLine($"  Address: {existingCustomer.Address}");
        Console.WriteLine($"  Email: {existingCustomer.Email}");
        Console.WriteLine();

        Console.Write("Enter New FullName: ");
        string? fullName = Console.ReadLine();
        Console.Write("Enter New Phone: ");
        string? phone = Console.ReadLine();
        Console.Write("Enter New Address: ");
        string? address = Console.ReadLine();
        Console.Write("Enter New Email: ");
        string? email = Console.ReadLine();
       

        _service.UpdateCustomer(new Customer
        {
           Id = id,
           FullName= fullName,
           Phone = phone,
           Address = address,
           Email = email

        });
        Console.WriteLine("\nCustomer updated successfully.");
        WaitForInput();
    }
    public static void DisplayCustomers()
    {
        Console.Clear();
        Console.WriteLine("---------------------------");
        Console.WriteLine("------ All Customers ------");
        Console.WriteLine("---------------------------");
        Console.WriteLine();

        var customers = _service.GetAllCustomers();
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            WaitForInput();
            return;
        }
        DisplayCustomersTable(customers);
        Console.WriteLine($"\nTotal: {customers.Count} customer(s)");
        WaitForInput();
    }
    public static void DisplayCustomersTable(List<Customer> customers)
    {
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        Console.WriteLine($"{"ID",-5} | {"FullName",-23} | {"Phone",-12} | {"Address",-24} | {"Email",-20}");
        Console.WriteLine("-----------------------------------------------------------------------------------------");

        foreach (var cust in customers)
        {
            Console.WriteLine($"{cust.Id,-5} | {cust.FullName,-23} | {cust.Phone,-12} | {cust.Address,-24} | {cust.Email,-20}");
        }

        Console.WriteLine("-------------------------------------------------------------------------------------------");
    }
    public static void SearchCustomerByIdConsole()
    {
        Console.Clear();
        Console.WriteLine("-----------------------------");
        Console.WriteLine("--- Search Customer By ID ---");
        Console.WriteLine("-----------------------------");
        Console.WriteLine();
        Console.Write("Enter Customer ID to search: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Error: Invalid ID format.");
            WaitForInput();
            return;
        }
        var (found, customer) = _service.SearchCustomerById(id);
        if (!found)
        {
            Console.WriteLine($"Customer with ID {id} not found.");
        }
        WaitForInput();
    }
}
