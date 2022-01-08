using System;
using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities
{
  public class Book : Entity<Guid>
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; } 
    [JsonIgnore] public Author Author { get; set; }
    [JsonIgnore] public Publisher Publisher { get; set; }
    [JsonIgnore] public Genre Genre { get; set; }
  }
}