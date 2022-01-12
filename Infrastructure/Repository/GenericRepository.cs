using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
  public sealed class GenericRepository<T> : IGenericRepository<T> where T : Domain.Entities.Base.DomainEntity
  {
    private readonly PersistenceContext _context;

    public GenericRepository(PersistenceContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context), $"{nameof(context)} is unavailable");
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool isTracking = false,
      string includeStringProperties = "")
    {
      IQueryable<T> query = _context.Set<T>();

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

    public async Task<T> FindAsync(object id)
    {
      return await _context.Set<T>().FindAsync(id);
    }

    public async Task<bool> ExistsAsync(object id)
    {
      return await _context.Set<T>().FindAsync(id) != null;
    }

    public async Task<T> CreateAsync(T entity)
    {
      _ = entity ?? throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} can not be null");
      _context.Set<T>().Add(entity);
      await CommitAsync();
      return entity;
    }

    public async Task UpdateAsync(T entity)
    {
      _context.Set<T>().Update(entity);
      await CommitAsync();
    }

    public async Task DeleteAsync(T entity)
    {
      _context.Set<T>().Remove(entity);
      await CommitAsync().ConfigureAwait(false);
    }

    public void ClearTracking()
    {
      _context.ChangeTracker.Clear();
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
  }
}