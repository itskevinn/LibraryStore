using System;
using System.ComponentModel.DataAnnotations;
using Application.Book.Dto;
using Application.Utilities;
using MediatR;

namespace Application.Book.Queries
{
  public record BookQuery([Required] Guid Id) : IRequest<Response<BookDto>>;
}