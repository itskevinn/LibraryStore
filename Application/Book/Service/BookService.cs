using System;
using System.Threading.Tasks;
using Application.Book.Dto;
using Application.Book.Request;
using AutoMapper;
using Domain.Repository;

namespace Application.Book.Service
{
  public class BookService
  {
    private readonly IGenericRepository<Domain.Entities.Book> _bookRepository;
    private IMapper _mapper;

    public BookService(IGenericRepository<Domain.Entities.Book> bookRepository, IMapper mapper)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), $"Mapper is unavailable");
      _bookRepository = bookRepository ??
                        throw new ArgumentNullException(nameof(bookRepository),
                          $"{nameof(bookRepository)} is unavailable");
    }

    public async Task<BookDto> CreateAsync(BookRequest bookRequest)
    {
      var book = _mapper.Map<BookRequest, Domain.Entities.Book>(bookRequest);
      var createdBook = await _bookRepository.CreateAsync(book);
      return _mapper.Map<Domain.Entities.Book, BookDto>(createdBook);
    }
  }
}