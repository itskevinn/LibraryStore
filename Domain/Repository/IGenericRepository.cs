using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities.Base;

namespace Domain.Repository
{
  public interface IGenericRepository<T> where T : DomainEntity
  {
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> FindAsync(object id);
    Task<bool> ExistsAsync(object id);

    Task<IEnumerable<T>> GetAsync(
      Expression<Func<T, bool>>? filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      bool isTracking = false,
      string includeStringProperties = "");

    void ClearTracking();
  }
}