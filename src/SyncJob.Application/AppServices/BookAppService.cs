using DTOs;
using Entitys;
using IAppServices;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AppServices
{
    public class BookAppService :AsyncCrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto,
            CreateUpdateBookDto, CreateUpdateBookDto>, IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository)
            : base(repository)
        {

        }
    }
}
