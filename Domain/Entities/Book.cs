using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Book : Entity<Guid>
  {
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public double Price { get; set; }
    public Guid AuthorId { get; set; }
    public Guid GenreId { get; set; }
    public Guid PublisherId { get; set; }
    [NotMapped] [JsonIgnore] public virtual Author Author { get; set; } = default!;
    [NotMapped] [JsonIgnore] public virtual Publisher Publisher { get; set; } = default!;
    [NotMapped] [JsonIgnore] public virtual Genre Genre { get; set; } = default!;
  }
}