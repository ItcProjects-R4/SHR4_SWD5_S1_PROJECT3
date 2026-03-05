using Microsoft.Data.SqlClient;
using SimpleStoryMangementSystem.Core.Models;

namespace SimpleStoryMangementSystem.DataLayer.Repositories;

public class OrderRepository
{
    public List<Order> GetAllOrders()
    {
        var orders = new List<Order>();

        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, CustomerId, OrderDate, TotalAmount FROM Orders", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            orders.Add(MapToOrder(reader));
        }

        return orders;
    }

    public (bool Found, Order? Order) GetOrderById(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, CustomerId, OrderDate, TotalAmount FROM Orders WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
            return (true, MapToOrder(reader));

        return (false, null);
    }

    public List<Order> GetOrdersByCustomerId(int customerId)
    {
        var orders = new List<Order>();

        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, CustomerId, OrderDate, TotalAmount FROM Orders WHERE CustomerId = @CustomerId", conn);
        cmd.Parameters.AddWithValue("@CustomerId", customerId);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            orders.Add(MapToOrder(reader));
        }

        return orders;
    }

    public void AddOrder(Order order)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
            VALUES (@CustomerId, @OrderDate, @TotalAmount);
            SELECT SCOPE_IDENTITY();", conn);

        cmd.Parameters.AddWithValue("@CustomerId",  order.CustomerId);
        cmd.Parameters.AddWithValue("@OrderDate",   order.OrderDate);
        cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

        order.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateOrder(Order order)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            UPDATE Orders
            SET CustomerId  = @CustomerId,
                OrderDate   = @OrderDate,
                TotalAmount = @TotalAmount
            WHERE Id = @Id", conn);

        cmd.Parameters.AddWithValue("@Id",          order.Id);
        cmd.Parameters.AddWithValue("@CustomerId",  order.CustomerId);
        cmd.Parameters.AddWithValue("@OrderDate",   order.OrderDate);
        cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"Order with ID {order.Id} not found.");
    }

   
    public decimal RecalculateOrderTotal(int orderId)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var calcCmd = new SqlCommand(@"
            SELECT ISNULL(SUM(Quantity * UnitPrice), 0)
            FROM OrderDetails
            WHERE OrderId = @OrderId", conn);
        calcCmd.Parameters.AddWithValue("@OrderId", orderId);

        decimal total = (decimal)calcCmd.ExecuteScalar();

        using var updateCmd = new SqlCommand(@"
            UPDATE Orders
            SET TotalAmount = @TotalAmount
            WHERE Id = @Id", conn);
        updateCmd.Parameters.AddWithValue("@TotalAmount", total);
        updateCmd.Parameters.AddWithValue("@Id",          orderId);
        updateCmd.ExecuteNonQuery();

        return total;
    }

    public void DeleteOrder(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("DELETE FROM Orders WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"Order with ID {id} not found.");
    }

    private static Order MapToOrder(SqlDataReader reader) => new Order
    {
        Id          = reader.GetInt32(0),
        CustomerId  = reader.GetInt32(1),
        OrderDate   = reader.GetDateTime(2),
        TotalAmount = reader.GetDecimal(3)
    };
}
