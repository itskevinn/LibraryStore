using System;

namespace Domain.Entities
{
  public interface IPerson
  {
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime BirthDate { get; set; }
    public int Age { get; }
  }
}