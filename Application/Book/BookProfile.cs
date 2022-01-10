using Application.Book.Dto;
using Application.Book.Request;
using AutoMapper;

namespace Application.Book
{
  public class BookProfile : Profile
  {
    public BookProfile()
    {
      CreateMap<Domain.Entities.Book, BookDto>().ReverseMap();
      CreateMap<Domain.Entities.Book, BookRequest>().ReverseMap();
    }
  }
}