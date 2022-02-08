using System;
using Application.Author.Dto;
using Application.Genre.Dto;
using Application.Publisher.Dto;

namespace Application.Book.Dto
{
  public class BookDto
  {
    public Guid Id { get; set; }
    public double Price { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public PublisherDto Publisher { get; set; } = default!;
    public GenreDto Genre { get; set; } = default!;
    public AuthorDto Author { get; set; } = default!;
  }
}