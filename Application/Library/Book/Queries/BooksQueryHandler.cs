using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Book.Dto;
using Application.Utilities;
using AutoMapper;
using Domain.Services;
using MediatR;

namespace Application.Book.Queries
{
  public class BooksQueryHandler : IRequestHandler<BooksQuery, Response<IEnumerable<BookDto>>>
  {
    private readonly BookService _bookService;
    private readonly IMapper _mapper;

    public BooksQueryHandler(BookService bookService, IMapper mapper)
    {
      _bookService = bookService;
      _mapper = mapper;
    }

    public async Task<Response<IEnumerable<BookDto>>> Handle(BooksQuery request, CancellationToken cancellationToken)
    {
      _ = request ?? throw new ArgumentNullException(nameof(request), "request object needed to handle this task");
      var books = await _bookService
        .GetAsync(b => b.Status, null, false, "Author,Genre,Publisher");
      var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
      var response =
        new Response<IEnumerable<BookDto>>(HttpStatusCode.OK, "Book retrieved successfully", true, booksDto);
      return response;
    }
  }
}