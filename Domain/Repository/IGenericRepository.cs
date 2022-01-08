using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities.Base;

namespace Domain.Repository
{
  public interface IGenericRepository<TE> where TE : DomainEntity
  {
    Task<TE> CreateAsync(TE entity);
    Task UpdateAsync(TE entity);
    Task DeleteAsync(TE entity);
    Task<TE> GetByIdAsync(object id);
    Task<bool> ExistsAsync(object id);

    Task<IEnumerable<TE>> GetAsync(
      Expression<Func<TE, bool>>? filter = null,
      Func<IQueryable<TE>, IOrderedQueryable<TE>>? orderBy = null,
      bool isTracking = false,
      string includeStringProperties = "");
  }
}