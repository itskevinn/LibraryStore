using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repository;
using Domain.Services.Base.Base;

namespace Domain.Services
{
  [DomainService]
  public class AuthorService
  {
    private readonly IGenericRepository<Author> _authorRepository;

    public AuthorService(IGenericRepository<Author> authorRepository)
    {
      _authorRepository = authorRepository ??
                          throw new ArgumentNullException(nameof(authorRepository),
                            $"{nameof(authorRepository)} is unavailable");
    }

    public async Task<Author> Create(Author author)
    {
      try
      {
        return await _authorRepository.CreateAsync(author);
      }
      catch (Exception e)
      {
        throw new AppException("Oops! Something went wrong", e);
      }
    }

    public async Task UpdateAsync(Author author)
    {
      try
      {
        await _authorRepository.UpdateAsync(author);
      }
      catch (Exception e)
      {
        throw new AppException("Oops! Something went wrong", e);
      }
    }

    public async Task DeleteAsync(Author author)
    {
      try
      {
        await _authorRepository.DeleteAsync(author);
      }
      catch (Exception e)
      {
        throw new AppException("Oops! Something went wrong", e);
      }
    }

    public async Task<IEnumerable<Author>> GetAsync()
    {
      return await _authorRepository.GetAsync();
    }

    public async Task<Author> GetByIdAsync(Guid id)
    {
      return await _authorRepository.GetByIdAsync(id);
    }
  }
}