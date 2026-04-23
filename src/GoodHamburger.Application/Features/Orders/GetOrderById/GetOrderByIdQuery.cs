using MediatR;

namespace GoodHamburger.Application.Features.Orders.GetOrderById;

public record OrderItemDto(Guid ProductId, string ProductName, decimal Price, string Category);

public record OrderDto(
    Guid Id,
    DateTime CreatedAt,
    decimal SubTotal,
    decimal Discount,
    decimal Total,
    IEnumerable<OrderItemDto> Items);

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;