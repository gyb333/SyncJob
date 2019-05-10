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
            CreateMap<User,UserTarget>()
                .ForMember(p => p.Id, o => o.MapFrom(s => s.UserID))
                .ForMember(p => p.CreationTime, o => o.Ignore())
                .ForMember(p => p.LastModificationTime,o =>o.Ignore())
                .ForMember(p =>p.IsDisabled,o =>o.MapFrom(s =>s.IsValid))

                ;

        }
    }
}