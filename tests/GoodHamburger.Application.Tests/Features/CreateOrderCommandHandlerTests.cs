using FluentAssertions;
using GoodHamburger.Application.Features.Orders.CreateOrder;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Moq;

namespace GoodHamburger.Application.Tests.Features;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateOrderAndReturnId_WhenProductsExist()
    {
        // Arrange
        var mockProductRepo = new Mock<IProductRepository>();
        var mockOrderRepo = new Mock<IOrderRepository>();

        var productId = Guid.NewGuid();
        var product = new Product(productId, "X Burger", 5m, ProductCategory.Sandwich);

        mockProductRepo.Setup(repo => repo.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Product> { product });

        var handler = new CreateOrderCommandHandler(mockProductRepo.Object, mockOrderRepo.Object);
        var command = new CreateOrderCommand(new List<Guid> { productId });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty(); // O ID do pedido gerado não pode ser vazio

        mockOrderRepo.Verify(repo => repo.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}