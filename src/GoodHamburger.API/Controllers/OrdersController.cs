using GoodHamburger.Application.Features.Orders.CreateOrder;
using GoodHamburger.Application.Features.Orders.DeleteOrder;
using GoodHamburger.Application.Features.Orders.GetOrderById;
using GoodHamburger.Application.Features.Orders.GetOrders;
using GoodHamburger.Application.Features.Orders.UpdateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { OrderId = orderId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));

        if (order is null)
        {
            return NotFound(new { message = "O pedido informado não foi encontrado." });
        }

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _mediator.Send(new GetOrdersQuery());
        return Ok(orders);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { message = "O ID da rota não coincide com o ID do payload." });
        }

        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(new { message = "O pedido informado não foi encontrado para atualização." });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderCommand(id));

        if (!result)
        {
            return NotFound(new { message = "O pedido informado não foi encontrado para exclusão." });
        }

        return NoContent();
    }
}