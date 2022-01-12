using System;

namespace Application.Genre.Dto
{
  public class GenreDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
  }
}