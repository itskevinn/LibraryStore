using System;

namespace Domain.Entities.Base
{
  public interface IEntity<T>
  {
    T Id { get; set; }
    string CreatedBy { get; set; }
    string UpdatedBy { get; set; }
    DateTime CreationDate { get; set; }
    DateTime LastModificationDate { get; set; }
  }
}