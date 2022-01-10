using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base
{
  public class Entity<T> : DomainEntity, IEntity<T>
  {
    [Key] public T Id { get; set; }
    public bool State { get; set; }
    [Required] public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastModifiedOn { get; set; }
  }
}