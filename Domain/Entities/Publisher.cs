using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Publisher : Entity<Guid>
  {
    public string Name { get; set; } = default!;
  }
}