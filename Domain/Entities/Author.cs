using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Author : Person
  {
    public virtual IEnumerable<Book> Books { get; set; } = default!;
  }
}