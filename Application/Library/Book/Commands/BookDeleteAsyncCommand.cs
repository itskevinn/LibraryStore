using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Book.Commands
{
  public record BookDeleteAsyncCommand([Required] Guid Id) : IRequest;
}