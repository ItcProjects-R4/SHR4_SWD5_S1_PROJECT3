using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Core.Models;

public  class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; } = 0;

    public override string ToString()
    {
        return $"Order Id: {Id}, CustomerId: {CustomerId}, OrderDate: {OrderDate:d}, TotalAmount: {TotalAmount:C}";
    }
    public decimal CulcalateTotalAmount(decimal Amount)
    {
        TotalAmount += Amount;
        return TotalAmount;
    }
}
