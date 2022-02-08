using System.Collections.Generic;
using Application.Book.Dto;
using Application.Utilities;
using MediatR;

namespace Application.Book.Queries
{
  public record BooksQuery() : IRequest<Response<IEnumerable<BookDto>>>;
}