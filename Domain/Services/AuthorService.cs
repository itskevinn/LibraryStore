using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repository;
using Domain.Services.Base;

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