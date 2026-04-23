using FluentValidation;

namespace GoodHamburger.Application.Features.Orders.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("O ID do pedido é obrigatório.");
        RuleFor(x => x.ProductIds).NotEmpty().WithMessage("O pedido deve conter pelo menos um item.");
    }
}