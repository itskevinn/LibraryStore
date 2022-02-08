using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services;
using MediatR;

namespace Application.Book.Commands
{
  public class BookDeleteAsyncCommandHandler : AsyncRequestHandler<BookDeleteAsyncCommand>
  {
    private readonly IMapper _mapper;
    private readonly BookService _bookService;

    public BookDeleteAsyncCommandHandler(IMapper mapper, BookService bookService)
    {
      _mapper = mapper;
      _bookService = bookService;
    }

    protected override async Task Handle(BookDeleteAsyncCommand request, CancellationToken cancellationToken)
    {
      _ = request ?? throw new ArgumentNullException(nameof(request), "request object needed to handle this task");
      var book = await _bookService.FindAsync(request.Id);
      if (book == null) throw new NullReferenceException("Book does not exist");
      book.Status = false;
      await _bookService.UpdateAsync(book);
    }
  }
}