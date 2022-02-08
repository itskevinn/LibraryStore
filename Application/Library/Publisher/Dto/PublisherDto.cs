using System;
using System.Collections.Generic;
using Application.Book.Dto;

namespace Application.Publisher.Dto
{
  public class PublisherDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
  }
}