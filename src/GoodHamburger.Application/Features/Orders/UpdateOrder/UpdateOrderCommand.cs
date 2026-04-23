using MediatR;

namespace GoodHamburger.Application.Features.Orders.UpdateOrder;

public record UpdateOrderCommand(Guid Id, List<Guid> ProductIds) : IRequest<bool>;