using Application.Genre.Dto;
using AutoMapper;

namespace Application.Genre
{
  public class GenreProfile : Profile
  {
    public GenreProfile()
    {
      CreateMap<Domain.Entities.Genre, GenreDto>().ReverseMap();
    }
  }
}