using System;
using System.Collections.Generic;
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

    public async Task<IEnumerable<Book>> GetAsync()
    {
      return await _bookRepository.GetAsync();
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
      return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<Book> Create(Book book)
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
        await _bookRepository.UpdateAsync(book);
      }
      catch (Exception e)
      {
        throw new AppException("Oops! Something went wrong", e);
      }
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