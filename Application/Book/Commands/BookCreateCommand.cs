using System;
using System.ComponentModel.DataAnnotations;
using Application.Book.Dto;
using Application.Utilities;
using MediatR;

namespace Application.Person.Commands
{
  public record BookCreateCommand(
    Guid Id,
    double Price,
    string Name,
    string Description,
    string CreatedBy,
    Guid PublisherId,
    Guid GenreId,
    Guid AuthorId
  ) : IRequest<Response<BookDto>>;
}