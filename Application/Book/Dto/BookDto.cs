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
    public PublisherDto Publisher { get; set; }
    public GenreDto Genre { get; set; }
    public AuthorDto Author { get; set; }
  }
}