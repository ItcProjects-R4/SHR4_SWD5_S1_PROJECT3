using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStoryMangementSystem.Core.Models;

public class OrderDetails
{
    public int id { get; set; }
    public int orderId { get; set; }
    public int productId { get; set; }
    public int quantity { get; set; } = 0;
    public decimal UnitPrice { get; set; } = 0;
    public decimal SubTotal { get; set; } = 0;

    public override string ToString()
    {
        return $"OrderDetails Id: {id}, OrderId: {orderId}, ProductId: {productId}, Quantity: {quantity}, UnitPrice: {UnitPrice:C}, SubTotal: {SubTotal:C}";
    }


}

