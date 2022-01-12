using System;
using MediatR;

namespace Application.Book.Commands
{
  public record BookUpdateAsyncCommand(
    Guid Id,
    double Price,
    string Name,
    string Description,
    string UpdatedBy,
    bool State,
    Guid PublisherId,
    Guid GenreId,
    Guid AuthorId
  ) : IRequest;
}