using MediatR;

namespace GoodHamburger.Application.Features.Orders.CreateOrder;

public record CreateOrderCommand(List<Guid> ProductIds) : IRequest<Guid>;