using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly List<Customer> _customers;
    private int _nextId;

    List<Customer> Customerlist = new List<Customer>(
        [
            new Customer { Id = 1, FullName = "Kamal mohamed", Phone = "01099464373", Address = "37Fathy habib Street", Email = "kamal@gmail.com" },
            new Customer { Id = 2, FullName = "Ahmed mohamed", Phone = "011125623541", Address = "31Fathy mohamed Street", Email = "ahmed@gmail.com" },
            new Customer { Id = 3, FullName = "Mostafa mohamed", Phone = "01236121502", Address = "21 Momtaz  Street", Email = "mostafa@gmail.com" },
            new Customer { Id = 4, FullName = "Yasser mohamed", Phone = "01099464373", Address = "37Sayed Kazem Street", Email = "yasser@gmail.com" },
            new Customer { Id = 5, FullName = "Omar mohamed", Phone = "011125623541", Address = "31Salwa mohamed Street", Email = "omar@gmail.com" },
            new Customer { Id = 6, FullName = "Ali mohamed", Phone = "01236121502", Address = "21 Xavi  Street", Email = "ali@gmail.com" },
            new Customer { Id = 7, FullName = "Hassan mohamed", Phone = "01099464373", Address = "10Messi  Street", Email = "hassan@gmail.com" },
            new Customer { Id = 8, FullName = "Hussein mohamed", Phone = "011125623541", Address = "11Nemar  Street", Email = "hussein@gmail.com" },
            new Customer { Id = 9, FullName = "Mahmoud mohamed", Phone = "01236121502", Address = "21 Veratie  Street", Email = "mahmoud@gmail.com" },
            new Customer { Id = 10, FullName = "Sayed mohamed", Phone = "01099464373", Address = "37Fathy habib Street", Email = "sayed@gmail.com" }
            ]);

    public CustomerService()
    {
        _customers = Customerlist;
        _nextId = _customers.Count + 1;
    }
    public (bool, Customer) SearchCustomerById(int id)
    {
        foreach (var customer in _customers)
        {
            if (customer.Id == id)
            {
                Console.WriteLine($"Customer found: {customer.Id, -5} {customer.FullName, -20} {customer.Phone, -15} {customer.Address, -30} {customer.Email, -25}");
                return (true, customer);
                
            }
        }
        return (false, null);
    }
    public void AddCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        customer.Id = _nextId++;
        _customers.Add(customer);
    }
    public List<Customer> GetAllCustomers()
    {
        return new List<Customer>(_customers);
    }
    public void UpdateCustomer(Customer customer)
    {
        var Founded = _customers.FirstOrDefault(c => c.Id == customer.Id);
        if (Founded == null)
            throw new ArgumentException($"Customer with ID {customer.Id} not found.");

        Founded.FullName = customer.FullName;
        Founded.Phone = customer.Phone;
        Founded.Email = customer.Email;
        Founded.Address = customer.Address;
    }

    public void DeleteCustomer(int id)
    {
        var (found, customer) = SearchCustomerById(id);
        if (customer == null)
            throw new ArgumentException($"Customer with ID {id} not found.");

        _customers.Remove(customer);
    }
    
   

   
}
