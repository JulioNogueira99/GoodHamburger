using MediatR;

namespace GoodHamburger.Application.Features.Menu.GetMenu;

public record MenuDto(Guid Id, string Name, decimal Price, string Category);

public record GetMenuQuery() : IRequest<IEnumerable<MenuDto>>;