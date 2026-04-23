using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Tests.Entities;

public class OrderTests
{
    private static Product CreateSandwich(decimal price = 5m) =>
        new(Guid.NewGuid(), "X Burger", price, ProductCategory.Sandwich);

    private static Product CreateFries(decimal price = 2m) =>
        new(Guid.NewGuid(), "Batata", price, ProductCategory.Fries);

    private static Product CreateSoda(decimal price = 2.5m) =>
        new(Guid.NewGuid(), "Refrigerante", price, ProductCategory.Soda);

    [Fact]
    public void AddItem_ShouldThrowDomainException_WhenAddingDuplicateCategory()
    {
        // Arrange
        var order = new Order();
        var firstSandwich = CreateSandwich();
        var secondSandwich = CreateSandwich();
        order.AddItem(firstSandwich);

        // Act
        Action act = () => order.AddItem(secondSandwich);

        // Assert
        act.Should().Throw<DomainException>()
           .WithMessage("*Apenas um item por categoria é permitido*");
    }

    [Fact]
    public void AddItem_ShouldCalculate20PercentDiscount_WhenComboIsComplete()
    {
        // Arrange
        var order = new Order();
        var sandwich = CreateSandwich(5m);
        var fries = CreateFries(2m);
        var soda = CreateSoda(3m);

        // Act
        order.AddItem(sandwich);
        order.AddItem(fries);
        order.AddItem(soda);

        // Assert
        // Subtotal: 5 + 2 + 3 = 10. Desconto 20%: 2. Total: 8.
        order.SubTotal.Should().Be(10m);
        order.Discount.Should().Be(2m);
        order.Total.Should().Be(8m);
    }

    [Fact]
    public void AddItem_ShouldCalculate15PercentDiscount_WhenHasSandwichAndSoda()
    {
        // Arrange
        var order = new Order();
        var sandwich = CreateSandwich(5m);
        var soda = CreateSoda(5m);

        // Act
        order.AddItem(sandwich);
        order.AddItem(soda);

        // Assert
        // Subtotal: 10. Desconto 15%: 1.50. Total: 8.50.
        order.SubTotal.Should().Be(10m);
        order.Discount.Should().Be(1.50m);
        order.Total.Should().Be(8.50m);
    }

    [Fact]
    public void AddItem_ShouldCalculate10PercentDiscount_WhenHasSandwichAndFries()
    {
        // Arrange
        var order = new Order();
        var sandwich = CreateSandwich(6m);
        var fries = CreateFries(4m);

        // Act
        order.AddItem(sandwich);
        order.AddItem(fries);

        // Assert
        // Subtotal: 10. Desconto 10%: 1. Total: 9.
        order.SubTotal.Should().Be(10m);
        order.Discount.Should().Be(1m);
        order.Total.Should().Be(9m);
    }

    [Fact]
    public void RemoveItem_ShouldRecalculateTotalsAndRemoveDiscount_WhenComboIsBroken()
    {
        // Arrange
        var order = new Order();
        var sandwich = CreateSandwich(5m);
        var fries = CreateFries(5m);

        order.AddItem(sandwich);
        order.AddItem(fries);

        order.Total.Should().Be(9m);

        // Act
        order.RemoveItem(fries.Id);

        // Assert
        // Depois de remover a batata, fica só o sanduíche sem desconto
        order.Items.Should().HaveCount(1);
        order.SubTotal.Should().Be(5m);
        order.Discount.Should().Be(0m);
        order.Total.Should().Be(5m);
    }
}