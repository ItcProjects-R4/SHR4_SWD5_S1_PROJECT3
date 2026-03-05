using SimpleStoryMangementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Services.Interfaces;

public interface IProdectServices
{
    void AddProduct(Product product);
    void DeleteProduct(int Id);
    void UpdateProduct(Product product);

    List<Product> GetAllProducts();

    bool SearchProductById(int id);
}
