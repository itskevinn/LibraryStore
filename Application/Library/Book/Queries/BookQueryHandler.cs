using System;
using System.Linq;
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
  public class BookQueryHandler : IRequestHandler<BookQuery, Response<BookDto>>
  {
    private readonly IMapper _mapper;
    private readonly BookService _bookService;

    public BookQueryHandler(IMapper mapper, BookService bookService)
    {
      _mapper = mapper;
      _bookService = bookService;
    }

    public async Task<Response<BookDto>> Handle(BookQuery request, CancellationToken cancellationToken)
    {
      _ = request ?? throw new ArgumentNullException(nameof(request), "request object needed to handle this task");
      var books = await _bookService
          .GetAsync(b => b.Id == request.Id && b.Status, null, false, "Author,Genre,Publisher");
      var bookInfo = books.FirstOrDefault();
      var bookDto = _mapper.Map<BookDto>(bookInfo);
      var response = new Response<BookDto>(HttpStatusCode.OK, "Book retrieved successfully", true, bookDto);
      return response;
    }
  }
}