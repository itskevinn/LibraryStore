using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repository;
using Domain.Services.Base;

namespace Domain.Services
{
  [DomainService]
  public class BookService
  {
    private readonly IGenericRepository<Book> _bookRepository;

    public BookService(IGenericRepository<Book> bookRepository)
    {
      _bookRepository = bookRepository ??
                        throw new ArgumentNullException(nameof(bookRepository),
                          $"{nameof(bookRepository)} is unavailable");
    }

    public async Task<IEnumerable<Book>> GetAsync(Expression<Func<Book, bool>>? filter = null,
      Func<IQueryable<Book>, IOrderedQueryable<Book>>? orderBy = null,
      bool isTracking = false,
      string includeStringProperties = "")
    {
      return await _bookRepository.GetAsync(filter, orderBy, isTracking, includeStringProperties);
    }

    public async Task<Book> FindAsync(Guid id)
    {
      return await _bookRepository.FindAsync(id);
    }

    public async Task<Book> CreateAsync(Book book)
    {
      try
      {
        return await _bookRepository.CreateAsync(book);
      }
      catch (Exception e)
      {
        throw new AppException("Oops! Something went wrong", e);
      }
    }

    public async Task UpdateAsync(Book book)
    {
      try
      {
        _bookRepository.ClearTracking();
        await _bookRepository.UpdateAsync(book);
      }
      catch (Exception e)
      {
        throw new AppException("Oops! Something went wrong", e);
      }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
      return await _bookRepository.ExistsAsync(id);
    }

    public async Task DeleteAsync(Book book)
    {
      try
      {
        await _bookRepository.DeleteAsync(book);
      }
      catch (Exception e)
      {
        throw new AppException("Oops! Something went wrong", e);
      }
    }
  }
}