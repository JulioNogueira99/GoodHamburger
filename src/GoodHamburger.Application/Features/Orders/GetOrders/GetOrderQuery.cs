using MediatR;

namespace GoodHamburger.Application.Features.Orders.GetOrders;

public record OrderSummaryDto(
    Guid Id,
    DateTime CreatedAt,
    decimal Total,
    int ItemsCount);

public record GetOrdersQuery() : IRequest<IEnumerable<OrderSummaryDto>>;