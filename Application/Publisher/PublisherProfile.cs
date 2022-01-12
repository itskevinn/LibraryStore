using Application.Publisher.Dto;
using AutoMapper;

namespace Application.Publisher
{
  public class PublisherProfile : Profile
  {
    public PublisherProfile()
    {
      CreateMap<Domain.Entities.Publisher, PublisherDto>().ReverseMap();

    }
  }
}