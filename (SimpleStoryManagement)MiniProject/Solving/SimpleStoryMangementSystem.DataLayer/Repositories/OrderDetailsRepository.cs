using Microsoft.Data.SqlClient;
using SimpleStoryMangementSystem.Core.Models;

namespace SimpleStoryMangementSystem.DataLayer.Repositories;

public class OrderDetailsRepository
{
    public List<OrderDetails> GetAllOrderDetails()
    {
        var list = new List<OrderDetails>();

        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, OrderId, ProductId, Quantity, UnitPrice, SubTotal FROM OrderDetails", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(MapToOrderDetails(reader));
        }

        return list;
    }

    public (bool Found, OrderDetails? OrderDetails) GetOrderDetailsById(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, OrderId, ProductId, Quantity, UnitPrice, SubTotal FROM OrderDetails WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
            return (true, MapToOrderDetails(reader));

        return (false, null);
    }

  
    public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
    {
        var list = new List<OrderDetails>();

        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            SELECT Id, OrderId, ProductId, Quantity, UnitPrice, SubTotal
            FROM OrderDetails
            WHERE OrderId = @OrderId", conn);
        cmd.Parameters.AddWithValue("@OrderId", orderId);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(MapToOrderDetails(reader));
        }

        return list;
    }


    public void AddOrderDetails(OrderDetails orderDetails)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice)
            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);
            SELECT SCOPE_IDENTITY();", conn);

        cmd.Parameters.AddWithValue("@OrderId",   orderDetails.orderId);
        cmd.Parameters.AddWithValue("@ProductId", orderDetails.productId);
        cmd.Parameters.AddWithValue("@Quantity",  orderDetails.quantity);
        cmd.Parameters.AddWithValue("@UnitPrice", orderDetails.UnitPrice);

        orderDetails.id = Convert.ToInt32(cmd.ExecuteScalar());

        orderDetails.SubTotal = orderDetails.quantity * orderDetails.UnitPrice;
    }

    public void UpdateOrderDetails(OrderDetails orderDetails)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            UPDATE OrderDetails
            SET OrderId   = @OrderId,
                ProductId = @ProductId,
                Quantity  = @Quantity,
                UnitPrice = @UnitPrice
            WHERE Id = @Id", conn);

        cmd.Parameters.AddWithValue("@Id",        orderDetails.id);
        cmd.Parameters.AddWithValue("@OrderId",   orderDetails.orderId);
        cmd.Parameters.AddWithValue("@ProductId", orderDetails.productId);
        cmd.Parameters.AddWithValue("@Quantity",  orderDetails.quantity);
        cmd.Parameters.AddWithValue("@UnitPrice", orderDetails.UnitPrice);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"OrderDetails with ID {orderDetails.id} not found.");
    }

    public void DeleteOrderDetails(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("DELETE FROM OrderDetails WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"OrderDetails with ID {id} not found.");
    }

   
    public decimal CalculateSubTotal(int orderDetailsId)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            SELECT Quantity * UnitPrice AS SubTotal
            FROM OrderDetails
            WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", orderDetailsId);

        var result = cmd.ExecuteScalar();
        return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
    }

 
    public decimal CalculateOrderTotal(int orderId)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            SELECT ISNULL(SUM(Quantity * UnitPrice), 0) AS TotalAmount
            FROM OrderDetails
            WHERE OrderId = @OrderId", conn);
        cmd.Parameters.AddWithValue("@OrderId", orderId);

        return Convert.ToDecimal(cmd.ExecuteScalar());
    }

   
    public decimal CalculateTotalAmountForCustomer(int customerId)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            SELECT ISNULL(SUM(od.Quantity * od.UnitPrice), 0) AS TotalSpent
            FROM OrderDetails od
            INNER JOIN Orders o ON od.OrderId = o.Id
            WHERE o.CustomerId = @CustomerId", conn);
        cmd.Parameters.AddWithValue("@CustomerId", customerId);

        return Convert.ToDecimal(cmd.ExecuteScalar());
    }

    
    public List<CustomerOrderSummary> GetOrderSummaryForCustomer(int customerId)
    {
        var summaries = new List<CustomerOrderSummary>();

        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            SELECT
                o.Id            AS OrderId,
                o.OrderDate,
                p.ProductName,
                od.Quantity,
                od.UnitPrice,
                od.Quantity * od.UnitPrice  AS SubTotal,
                o.TotalAmount   AS OrderTotal
            FROM Orders o
            INNER JOIN OrderDetails od ON o.Id = od.OrderId
            INNER JOIN Products p     ON od.ProductId = p.Id
            WHERE o.CustomerId = @CustomerId
            ORDER BY o.Id, p.ProductName", conn);

        cmd.Parameters.AddWithValue("@CustomerId", customerId);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            summaries.Add(new CustomerOrderSummary
            {
                OrderId      = reader.GetInt32(0),
                OrderDate    = reader.GetDateTime(1),
                ProductName  = reader.GetString(2),
                Quantity     = reader.GetInt32(3),
                UnitPrice    = reader.GetDecimal(4),
                SubTotal     = reader.GetDecimal(5),
                OrderTotal   = reader.GetDecimal(6)
            });
        }

        return summaries;
    }

    
    public decimal CalculateAndUpdateOrderTotal(int orderId)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        decimal total;
        using (var calcCmd = new SqlCommand(@"
            SELECT ISNULL(SUM(Quantity * UnitPrice), 0)
            FROM OrderDetails
            WHERE OrderId = @OrderId", conn))
        {
            calcCmd.Parameters.AddWithValue("@OrderId", orderId);
            total = Convert.ToDecimal(calcCmd.ExecuteScalar());
        }

        using (var updateCmd = new SqlCommand(@"
            UPDATE Orders SET TotalAmount = @Total WHERE Id = @OrderId", conn))
        {
            updateCmd.Parameters.AddWithValue("@Total",   total);
            updateCmd.Parameters.AddWithValue("@OrderId", orderId);
            updateCmd.ExecuteNonQuery();
        }

        return total;
    }

    private static OrderDetails MapToOrderDetails(SqlDataReader reader) => new OrderDetails
    {
        id        = reader.GetInt32(0),
        orderId   = reader.GetInt32(1),
        productId = reader.GetInt32(2),
        quantity  = reader.GetInt32(3),
        UnitPrice = reader.GetDecimal(4),
        SubTotal  = reader.GetDecimal(5)
    };
}

public class CustomerOrderSummary
{
    public int      OrderId     { get; set; }
    public DateTime OrderDate   { get; set; }
    public string   ProductName { get; set; } = string.Empty;
    public int      Quantity    { get; set; }
    public decimal  UnitPrice   { get; set; }
    public decimal  SubTotal    { get; set; }
    public decimal  OrderTotal  { get; set; }

    public override string ToString()
    {
        return $"Order#{OrderId} | {OrderDate:d} | {ProductName,-20} | Qty: {Quantity} | " +
               $"Price: {UnitPrice:C} | SubTotal: {SubTotal:C} | OrderTotal: {OrderTotal:C}";
    }
}
