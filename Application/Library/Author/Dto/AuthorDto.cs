using System;

namespace Application.Author.Dto
{
  public class AuthorDto
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
  }
}