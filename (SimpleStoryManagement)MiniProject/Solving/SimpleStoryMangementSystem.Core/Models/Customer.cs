using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Core.Models;

public class Customer
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Email { get; set; } = string.Empty;
    public override string ToString()
    {
        return $"ID: {Id} | Name: {FullName} | Phone: {Phone} | Address: {Address} | Email: {Email}";
    }
}
