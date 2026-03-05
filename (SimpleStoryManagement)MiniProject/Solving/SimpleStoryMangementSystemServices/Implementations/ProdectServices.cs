using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;

namespace SimpleStoryMangementSystem.Services.Implementations;

public class ProductServices : IProdectServices
{
    private readonly List<Product> _products;
    private int _nextId;

    List<Product> _seedData = new List<Product>(
    [
        new Product { Id = 1, ProductName = "Laptop",      Price = 15000m, StockQuantity = 50,  Category = "Electronics" },
        new Product { Id = 2, ProductName = "Mouse",       Price = 250m,   StockQuantity = 200, Category = "Electronics" },
        new Product { Id = 3, ProductName = "Keyboard",    Price = 450m,   StockQuantity = 150, Category = "Electronics" },
        new Product { Id = 4, ProductName = "Monitor",     Price = 5500m,  StockQuantity = 30,  Category = "Electronics" },
        new Product { Id = 5, ProductName = "Headphones",  Price = 1200m,  StockQuantity = 80,  Category = "Electronics" },
        new Product { Id = 6, ProductName = "USB Hub",     Price = 350m,   StockQuantity = 120, Category = "Accessories" }
    ]);

    public ProductServices()
    {
        _products = _seedData;
        _nextId = _products.Count + 1;
    }

    public void AddProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        product.Id = _nextId++;
        _products.Add(product);
    }

    public void DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) throw new ArgumentException($"Product with ID {id} not found.");
        _products.Remove(product);
    }

    public void UpdateProduct(Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existing == null) throw new ArgumentException($"Product with ID {product.Id} not found.");
        existing.ProductName   = product.ProductName;
        existing.Price         = product.Price;
        existing.StockQuantity = product.StockQuantity;
        existing.Category      = product.Category;
    }

    public List<Product> GetAllProducts()
        => new List<Product>(_products);

    public bool SearchProductById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Console.WriteLine(product);
            return true;
        }
        return false;
    }

    public (bool, Product) GetProductWithDetails(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return product != null ? (true, product) : (false, null!);
    }
}
