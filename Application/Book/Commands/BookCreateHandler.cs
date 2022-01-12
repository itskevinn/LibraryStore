using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Book.Dto;
using Application.Person.Commands;
using Application.Utilities;
using AutoMapper;
using Domain.Services;
using MediatR;

namespace Application.Book.Commands
{
  public class BookCreateHandler : IRequestHandler<BookCreateCommand, Response<BookDto>>
  {
    private readonly BookService _bookService;
    private readonly IMapper _mapper;

    public BookCreateHandler(BookService bookService, IMapper mapper)
    {
      _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
      _mapper = mapper;
    }

    public async Task<Response<BookDto>> Handle(BookCreateCommand request,
      CancellationToken cancellationToken)
    {
      _ = request ?? throw new ArgumentNullException(nameof(request), "request object needed to handle this task");
      try
      {
        Domain.Entities.Book book = new()
        {
          Description = request.Description,
          Name = request.Name,
          Price = request.Price,
          CreatedBy = "Admin",
          UpdatedBy = " ",
          Status = true,
          AuthorId = request.AuthorId,
          GenreId = request.GenreId,
          PublisherId = request.PublisherId
        };
        var createdBook = await _bookService.CreateAsync(book);
        var bookDto = _mapper.Map<BookDto>(createdBook);
        return new Response<BookDto>(HttpStatusCode.OK, "Book created successfully", true, bookDto);
      }
      catch (Exception e)
      {
        return new Response<BookDto>(HttpStatusCode.InternalServerError,
          $"Something went wrong {e.Message}", false);
      }
    }
  }
}