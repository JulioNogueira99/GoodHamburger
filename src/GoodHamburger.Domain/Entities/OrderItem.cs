using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public ProductCategory Category { get; private set; }

    protected OrderItem() { }

    public OrderItem(Guid productId, string productName, decimal price, ProductCategory category)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Category = category;
    }
}
