using System;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Genre : Entity<Guid>
  {
    public string Name { get; set; }
    public string Description { get; set; }
  }
}