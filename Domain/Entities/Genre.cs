using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Genre : Entity<Guid>
  {
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public virtual ICollection<Book> Books { get; set; } = default!;
  }
}