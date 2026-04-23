using FluentValidation;

namespace GoodHamburger.Application.Features.Orders.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.ProductIds)
            .NotEmpty().WithMessage("O pedido deve conter pelo menos um item.");
    }
}