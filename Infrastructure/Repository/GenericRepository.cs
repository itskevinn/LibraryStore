using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
  public sealed class GenericRepository<E> : IGenericRepository<E> where E : Domain.Entities.Base.DomainEntity, IDisposable
  {
    private readonly PersistenceContext _context;

    public GenericRepository(PersistenceContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null,
      Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, bool isTracking = false,
      string includeStringProperties = "")
    {
      IQueryable<E> query = _context.Set<E>();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      if (!string.IsNullOrEmpty(includeStringProperties))
      {
        query = includeStringProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
          .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
      }

      if (orderBy != null)
      {
        return await orderBy(query).ToListAsync().ConfigureAwait(false);
      }

      return (!isTracking) ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
    }

    public async Task<E> GetByIdAsync(object id)
    {
      return await _context.Set<E>().FindAsync(id);
    }

    public async Task<bool> ExistsAsync(object id)
    {
      return await _context.Set<E>().FindAsync(id) != null;
    }

    public async Task<E> CreateAsync(E entity)
    {
      _ = entity ?? throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} can not be null");
      _context.Set<E>().Add(entity);
      await CommitAsync();
      return entity;
    }

    public async Task UpdateAsync(E entity)
    {
      _context.Set<E>().Update(entity);
      await CommitAsync();
    }

    public async Task DeleteAsync(E entity)
    {
      _context.Set<E>().Remove(entity);
      await CommitAsync().ConfigureAwait(false);
    }

    private async Task CommitAsync()
    {
      _context.ChangeTracker.DetectChanges();

      foreach (var entry in _context.ChangeTracker.Entries())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
            break;
          case EntityState.Modified:
            entry.Property("LastModifiedOn").CurrentValue = DateTime.UtcNow;
            break;
        }
      }

      await _context.CommitAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      this._context.Dispose();
    }
  }
}