using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repository;

namespace Domain.Services
{
  public class PublisherService
  {
    private readonly IGenericRepository<Publisher> _publisherRepository;

    public PublisherService(IGenericRepository<Publisher> publisherRepository)
    {
      _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository),
        $"{nameof(publisherRepository)} is unavailable");
    }

    public async Task<IEnumerable<Publisher>> GetAsync()
    {
      return await _publisherRepository.GetAsync();
    }

    public async Task<Publisher> GetByIdAsync(Guid id)
    {
      return await _publisherRepository.GetByIdAsync(id);
    }
  }
}