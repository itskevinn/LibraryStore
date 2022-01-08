using System;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Author : Entity<Guid>, IPerson
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
  }
  
}