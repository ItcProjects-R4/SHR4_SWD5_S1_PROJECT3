using SimpleStoryMangementSystem.Core.Models;


namespace SimpleStoryMangementSystem.Services.Interfaces
{
  
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);

        void DeleteCustomer(int id);

        void UpdateCustomer(Customer customer);

        List<Customer> GetAllCustomers();

        (bool, Customer) SearchCustomerById(int id);


    }
}