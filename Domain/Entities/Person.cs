using System;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public abstract class Person : Entity<Guid>, IPerson
  {
    private const int TOTAL_YEAR_DAYS = 365;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; } = default!;
    public int Age => (int) Math.Round(DateTime.Now.Subtract(BirthDate).TotalDays / TOTAL_YEAR_DAYS);
  }
}