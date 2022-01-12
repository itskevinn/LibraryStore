using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repository;

namespace Domain.Services
{
  public class GenreService
  {
    private readonly IGenericRepository<Genre> _genreRepository;

    public GenreService(IGenericRepository<Genre> genreRepository)
    {
      _genreRepository = genreRepository ??
                         throw new ArgumentNullException(nameof(genreRepository),
                           $"{nameof(genreRepository)} is unavailable");
    }

    public async Task<IEnumerable<Genre>> GetAsync()
    {
      return await _genreRepository.GetAsync();
    }

    public async Task<Genre> GetByIdAsync(Guid id)
    {
      return await _genreRepository.FindAsync(id);
    }
  }
}