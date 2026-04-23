using GoodHamburger.Application.Features.Menu.GetMenu;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenu()
    {
        var result = await _mediator.Send(new GetMenuQuery());
        return Ok(result);
    }
}