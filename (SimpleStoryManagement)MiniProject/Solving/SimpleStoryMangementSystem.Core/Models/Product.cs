using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Core.Models;

public class Product
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Category { get; set; }
    public override string ToString()
    {
        return $"Product Id: {Id}, Name: {ProductName}, Price: {Price:C}, Stock: {StockQuantity}, Category: {Category}";
    }
}
