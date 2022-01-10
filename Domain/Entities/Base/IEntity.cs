using System;

namespace Domain.Entities.Base
{
  public interface IEntity<T>
  {
    T Id { get; set; }
    public bool State { get; set; }
    string CreatedBy { get; set; }
    string UpdatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    DateTime LastModifiedOn { get; set; }
  }
}