using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal SubTotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }

    public Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Product product)
    {
        if (_items.Any(i => i.Category == product.Category))
        {
            throw new DomainException($"O pedido já contém um item da categoria {product.Category}. Apenas um item por categoria é permitido.");
        }

        var orderItem = new OrderItem(product.Id, product.Name, product.Price, product.Category);
        _items.Add(orderItem);

        RecalculateTotals();
    }

    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
        {
            throw new DomainException("Item não encontrado no pedido.");
        }

        _items.Remove(item);
        RecalculateTotals();
    }

    private void RecalculateTotals()
    {
        SubTotal = _items.Sum(i => i.Price);
        Discount = CalculateDiscount();
        Total = SubTotal - Discount;
    }

    private decimal CalculateDiscount()
    {
        bool hasSandwich = _items.Any(i => i.Category == ProductCategory.Sandwich);
        bool hasFries = _items.Any(i => i.Category == ProductCategory.Fries);
        bool hasSoda = _items.Any(i => i.Category == ProductCategory.Soda);

        decimal sandwichPrice = _items.FirstOrDefault(i => i.Category == ProductCategory.Sandwich)?.Price ?? 0;
        decimal discountPercentage = 0m;

        if (hasSandwich && hasFries && hasSoda)
        {
            discountPercentage = 0.20m;
        }
        else if (hasSandwich && hasSoda)
        {
            discountPercentage = 0.15m;
        }
        else if (hasSandwich && hasFries)
        {
            discountPercentage = 0.10m;
        }

        return SubTotal * discountPercentage;
    }
}