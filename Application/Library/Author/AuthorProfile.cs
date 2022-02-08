using Application.Author.Dto;
using AutoMapper;

namespace Application.Author
{
  public class AuthorProfile : Profile
  {
    public AuthorProfile()
    {
      CreateMap<Domain.Entities.Author, AuthorDto>().ReverseMap();
    }
  }
}