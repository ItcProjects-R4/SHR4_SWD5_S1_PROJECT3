using SimpleStoryMangementSystem.Core.Models;
using SimpleStoryMangementSystem.Services.Interfaces;
using SimpleStoryMangementSystem.Services.Implementations;

namespace SimpleStoryMangementSystem.UI.UI.ProductUi;

public class ProductServicesUi
{
    public static IProdectServices _service = new ProductServices();

    public static void WaitForInput()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    public static void AddProductConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Add New Product ---");
        Console.Write("Enter Product Name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price)) { Console.WriteLine("Invalid price."); WaitForInput(); return; }
        Console.Write("Enter Stock Quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int stock)) { Console.WriteLine("Invalid quantity."); WaitForInput(); return; }
        Console.Write("Enter Category: ");
        string? category = Console.ReadLine();

        _service.AddProduct(new Product { ProductName = name, Price = price, StockQuantity = stock, Category = category });
        Console.WriteLine("\nProduct added successfully.");
        WaitForInput();
    }

    public static void DeleteProductConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Delete Product ---");
        DisplayProductsTable(_service.GetAllProducts());
        Console.Write("\nEnter Product ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }
        _service.DeleteProduct(id);
        Console.WriteLine("\nProduct deleted successfully.");
        WaitForInput();
    }

    public static void UpdateProductConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Edit Product ---");
        DisplayProductsTable(_service.GetAllProducts());
        Console.Write("\nEnter Product ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }

        Console.Write("Enter New Name: "); string? name = Console.ReadLine();
        Console.Write("Enter New Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price)) { Console.WriteLine("Invalid price."); WaitForInput(); return; }
        Console.Write("Enter New Stock: ");
        if (!int.TryParse(Console.ReadLine(), out int stock)) { Console.WriteLine("Invalid stock."); WaitForInput(); return; }
        Console.Write("Enter New Category: "); string? cat = Console.ReadLine();

        _service.UpdateProduct(new Product { Id = id, ProductName = name, Price = price, StockQuantity = stock, Category = cat });
        Console.WriteLine("\nProduct updated successfully.");
        WaitForInput();
    }

    public static void DisplayProducts()
    {
        Console.Clear();
        Console.WriteLine("--- All Products ---");
        var products = _service.GetAllProducts();
        if (products.Count == 0) { Console.WriteLine("No products found."); WaitForInput(); return; }
        DisplayProductsTable(products);
        Console.WriteLine($"\nTotal: {products.Count} product(s)");
        WaitForInput();
    }

    public static void SearchProductConsole()
    {
        Console.Clear();
        Console.WriteLine("--- Search Product By ID ---");
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); WaitForInput(); return; }
        bool found = _service.SearchProductById(id);
        if (!found) Console.WriteLine($"Product with ID {id} not found.");
        WaitForInput();
    }

    public static void DisplayProductsTable(List<Product> products)
    {
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine($"{"ID",-5} | {"Name",-20} | {"Price",-12} | {"Stock",-8} | {"Category",-15}");
        Console.WriteLine("------------------------------------------------------------------------");
        foreach (var p in products)
            Console.WriteLine($"{p.Id,-5} | {p.ProductName,-20} | {p.Price,-12} | {p.StockQuantity,-8} | {p.Category,-15}");
        Console.WriteLine("------------------------------------------------------------------------");
    }
}
