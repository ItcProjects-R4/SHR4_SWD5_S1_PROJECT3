using Microsoft.Data.SqlClient;
using SimpleStoryMangementSystem.Core.Models;

namespace SimpleStoryMangementSystem.DataLayer.Repositories;

public class CustomerRepository
{
    public List<Customer> GetAllCustomers()
    {
        var customers = new List<Customer>();

        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("SELECT Id, FullName, Phone, Address, Email FROM Customers", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            customers.Add(MapToCustomer(reader));
        }

        return customers;
    }

    public (bool Found, Customer? Customer) GetCustomerById(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, FullName, Phone, Address, Email FROM Customers WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return (true, MapToCustomer(reader));
        }

        return (false, null);
    }

    public void AddCustomer(Customer customer)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            INSERT INTO Customers (FullName, Phone, Address, Email)
            VALUES (@FullName, @Phone, @Address, @Email);
            SELECT SCOPE_IDENTITY();", conn);

        cmd.Parameters.AddWithValue("@FullName", customer.FullName ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Phone",    customer.Phone    ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Address",  customer.Address  ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Email",    customer.Email);

        customer.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateCustomer(Customer customer)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            UPDATE Customers
            SET FullName = @FullName,
                Phone    = @Phone,
                Address  = @Address,
                Email    = @Email
            WHERE Id = @Id", conn);

        cmd.Parameters.AddWithValue("@Id",       customer.Id);
        cmd.Parameters.AddWithValue("@FullName", customer.FullName ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Phone",    customer.Phone    ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Address",  customer.Address  ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Email",    customer.Email);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"Customer with ID {customer.Id} not found.");
    }

    public void DeleteCustomer(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("DELETE FROM Customers WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"Customer with ID {id} not found.");
    }

    private static Customer MapToCustomer(SqlDataReader reader) => new Customer
    {
        Id       = reader.GetInt32(0),
        FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
        Phone    = reader.IsDBNull(2) ? null : reader.GetString(2),
        Address  = reader.IsDBNull(3) ? null : reader.GetString(3),
        Email    = reader.GetString(4)
    };
}
