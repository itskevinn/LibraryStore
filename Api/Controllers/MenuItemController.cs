
using Application.Config.MenuItem.Commands;
using Application.Config.MenuItem.Dto;
using Application.Config.MenuItem.Queries.MenuItems;
using Application.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MenuItemController : Controller
  {
    private readonly IMediator _mediator;

    public MenuItemController(IMediator mediator)
    {
      _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult<Response<MenuItemDto>>> Post(MenuItemCreateCommand menuItem)
    => await _mediator.Send(menuItem);

    [HttpGet("GetByRole/{id:guid}")]
    public async Task<ActionResult<Response<IEnumerable<MenuItemDto>>>> GetByRole(Guid id)
    => await _mediator.Send(new MenuItems(id));
  }
}