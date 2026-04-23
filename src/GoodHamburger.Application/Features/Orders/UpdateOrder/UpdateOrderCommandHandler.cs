using GoodHamburger.Application.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Features.Orders.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order is null) return false;

        var products = await _productRepository.GetByIdsAsync(request.ProductIds, cancellationToken);
        if (products.Count() != request.ProductIds.Count)
        {
            throw new ArgumentException("Um ou mais produtos informados são inválidos.");
        }

        var currentItems = order.Items.Select(i => i.ProductId).ToList();
        foreach (var itemId in currentItems)
        {
            order.RemoveItem(itemId);
        }

        foreach (var productId in request.ProductIds)
        {
            var product = products.First(p => p.Id == productId);
            order.AddItem(product);
        }

        // 3. Salvamos as alterações
        await _orderRepository.UpdateAsync(order, cancellationToken);
        return true;
    }
}