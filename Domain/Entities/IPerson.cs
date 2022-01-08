using System;

namespace Domain.Entities
{
  public interface IPerson
  {
    private const int TOTAL_DAYS = 365;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age => (int) Math.Round(DateTime.Now.Subtract(BirthDate).TotalDays / TOTAL_DAYS);
  }
}