using MediatR;

namespace GoodHamburger.Application.Features.Orders.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : IRequest<bool>;