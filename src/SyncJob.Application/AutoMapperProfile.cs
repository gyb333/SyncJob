using AutoMapper;
using DTOs;
using Entitys;

namespace SyncJob
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, CreateUpdateBookDto>();
            //CreateMap<CreateUpdateBookDto, Book > ()
              
                
                ;

        }
    }
}