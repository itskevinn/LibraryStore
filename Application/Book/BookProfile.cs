using System.Collections.Generic;
using Application.Author.Dto;
using Application.Book.Commands;
using Application.Book.Dto;
using Application.Genre.Dto;
using Application.Person.Commands;
using Application.Publisher.Dto;
using AutoMapper;

namespace Application.Book
{
  public class BookProfile : Profile
  {
    public BookProfile()
    {
      CreateMap<Domain.Entities.Book, BookDto>().ReverseMap()
        .ForMember(book => book.Author, opts =>
          opts.MapFrom(book => new AuthorDto()
          {
            Id = book.Author.Id,
            BirthDate = book.Author.BirthDate,
            FirstName = book.Author.FirstName,
            LastName = book.Author.LastName
          }))
        .ForMember(book => book.Publisher, opts =>
          opts.MapFrom(book => new PublisherDto()
          {
            Id = book.Publisher.Id,
            Name = book.Publisher.Name,
          }))
        .ForMember(book => book.Genre, opt =>
          opt.MapFrom(book => new GenreDto()
          {
            Id = book.Genre.Id,
            Description = book.Genre.Description,
            Name = book.Genre.Name
          }));
      CreateMap<BookCreateCommand, Domain.Entities.Book>().ReverseMap();
      CreateMap<BookUpdateAsyncCommand, Domain.Entities.Book>().ReverseMap();
      CreateMap<BookDeleteAsyncCommand, Domain.Entities.Book>().ReverseMap();
    }
  }
}