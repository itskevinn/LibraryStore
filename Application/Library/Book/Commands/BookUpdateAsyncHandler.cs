using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services;
using MediatR;

namespace Application.Book.Commands
{
  public class BookUpdateAsyncHandler : AsyncRequestHandler<BookUpdateAsyncCommand>
  {
    private readonly BookService _bookService;
    private readonly IMapper _mapper;

    public BookUpdateAsyncHandler(BookService bookService, IMapper mapper)
    {
      _bookService = bookService;
      _mapper = mapper;
    }

    protected override async Task Handle(BookUpdateAsyncCommand request, CancellationToken cancellationToken)
    {
      _ = request ?? throw new ArgumentNullException(nameof(request), "request object needed to handle this task");
      var oldBook = await _bookService.FindAsync(request.Id);
      if (oldBook == null) throw new NullReferenceException("Book does not exist");
      Domain.Entities.Book newBook = _mapper.Map<Domain.Entities.Book>(request);
      newBook.CreatedBy = oldBook.CreatedBy;
      newBook.CreatedOn = oldBook.CreatedOn;
      newBook.LastModifiedOn = DateTime.UtcNow;
      await _bookService.UpdateAsync(newBook);
    }
  }
}