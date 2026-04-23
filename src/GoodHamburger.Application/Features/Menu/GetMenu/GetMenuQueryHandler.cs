using GoodHamburger.Application.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Features.Menu.GetMenu;

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, IEnumerable<MenuDto>>
{
    private readonly IProductRepository _productRepository;

    public GetMenuQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<MenuDto>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        return products.Select(p => new MenuDto(
            p.Id,
            p.Name,
            p.Price,
            p.Category.ToString()
        ));
    }
}