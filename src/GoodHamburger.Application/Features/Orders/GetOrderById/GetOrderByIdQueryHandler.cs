using GoodHamburger.Application.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Features.Orders.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

        if (order is null)
        {
            return null;
        }

        return new OrderDto(
            order.Id,
            order.CreatedAt,
            order.SubTotal,
            order.Discount,
            order.Total,
            order.Items.Select(i => new OrderItemDto(
                i.ProductId,
                i.ProductName,
                i.Price,
                i.Category.ToString()))
        );
    }
}