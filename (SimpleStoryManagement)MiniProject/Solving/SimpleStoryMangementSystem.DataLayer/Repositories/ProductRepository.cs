using Microsoft.Data.SqlClient;
using SimpleStoryMangementSystem.Core.Models;

namespace SimpleStoryMangementSystem.DataLayer.Repositories;

public class ProductRepository
{
    public List<Product> GetAllProducts()
    {
        var products = new List<Product>();

        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, ProductName, Price, StockQuantity, Category FROM Products", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            products.Add(MapToProduct(reader));
        }

        return products;
    }

    public (bool Found, Product? Product) GetProductById(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(
            "SELECT Id, ProductName, Price, StockQuantity, Category FROM Products WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
            return (true, MapToProduct(reader));

        return (false, null);
    }

    public void AddProduct(Product product)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            INSERT INTO Products (ProductName, Price, StockQuantity, Category)
            VALUES (@ProductName, @Price, @StockQuantity, @Category);
            SELECT SCOPE_IDENTITY();", conn);

        cmd.Parameters.AddWithValue("@ProductName",   product.ProductName    ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Price",         product.Price);
        cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
        cmd.Parameters.AddWithValue("@Category",      product.Category       ?? (object)DBNull.Value);

        product.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateProduct(Product product)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
            UPDATE Products
            SET ProductName   = @ProductName,
                Price         = @Price,
                StockQuantity = @StockQuantity,
                Category      = @Category
            WHERE Id = @Id", conn);

        cmd.Parameters.AddWithValue("@Id",            product.Id);
        cmd.Parameters.AddWithValue("@ProductName",   product.ProductName    ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Price",         product.Price);
        cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
        cmd.Parameters.AddWithValue("@Category",      product.Category       ?? (object)DBNull.Value);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"Product with ID {product.Id} not found.");
    }

    public void DeleteProduct(int id)
    {
        using var conn = DBHelper.GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("DELETE FROM Products WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new ArgumentException($"Product with ID {id} not found.");
    }

    private static Product MapToProduct(SqlDataReader reader) => new Product
    {
        Id            = reader.GetInt32(0),
        ProductName   = reader.IsDBNull(1) ? null : reader.GetString(1),
        Price         = reader.GetDecimal(2),
        StockQuantity = reader.GetInt32(3),
        Category      = reader.IsDBNull(4) ? null : reader.GetString(4)
    };
}
