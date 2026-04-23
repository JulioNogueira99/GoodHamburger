using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using MediatR;

namespace GoodHamburger.Application.Features.Orders.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByIdsAsync(request.ProductIds, cancellationToken);

        if (products.Count() != request.ProductIds.Count)
        {
            throw new ArgumentException("Um ou mais produtos informados são inválidos ou não existem no cardápio.");
        }

        var order = new Order();

        foreach (var productId in request.ProductIds)
        {
            var product = products.First(p => p.Id == productId);
            order.AddItem(product);
        }

        await _orderRepository.AddAsync(order, cancellationToken);

        return order.Id;
    }
}