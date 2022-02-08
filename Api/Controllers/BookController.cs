using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Library.Book.Commands;
using Application.Library.Book.Dto;
using Application.Library.Book.Queries;
using Application.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BookController : Controller
  {
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Response<BookDto>>> Post(BookCreateCommand book) =>
      await _mediator.Send(book);

    [HttpPut]
    [Authorize]
    public async Task Put(BookUpdateAsyncCommand book) => await _mediator.Send(book);

    [HttpDelete]
    [Authorize]
    public async Task Delete(BookDeleteAsyncCommand book) => await _mediator.Send(book);

    [HttpGet("GetById/{id:guid}")]
    [Authorize]
    public async Task<ActionResult<Response<BookDto>>> Get(Guid id) => await _mediator.Send(new BookQuery(id));

    [HttpGet("Get")]
    [Authorize]
    public async Task<ActionResult<Response<IEnumerable<BookDto>>>> Get() => await _mediator.Send(new BooksQuery());
  }
}