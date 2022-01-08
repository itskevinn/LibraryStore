using System;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Publisher : Entity<Guid>
  {
    public string Name { get; set; }
  }
}