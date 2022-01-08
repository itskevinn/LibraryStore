using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base
{
  public class Entity<T> : DomainEntity, IEntity<T>
  {
    [Key] public T Id { get; set; }
    [Required] public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    [Required] public DateTime CreationDate { get; set; }
    public DateTime LastModificationDate { get; set; }
  }
}