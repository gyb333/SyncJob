using DTOs;
using Entitys;
using IAppServices;
using Microsoft.AspNetCore.Authorization;
using Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AppServices
{
    [RemoteService(false)]
    [Authorize(BookPermissions.GroupName)]
    public class BookAppService :AsyncCrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto,
            CreateUpdateBookDto, CreateUpdateBookDto>, IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository)
            : base(repository)
        {

        }


        [Authorize(BookPermissions.Create)]
        public async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
        {
            //var product = await _bookManager.CreateAsync(
            //    input.Code,
            //    input.Name,
            //    input.Price,
            //    input.StockCount,
            //    input.ImageName
            //);

            return ObjectMapper.Map<Book, BookDto>(null);
        }

        [Authorize(BookPermissions.Update)]
        public async Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto input)
        {
            //var product = await _productRepository.GetAsync(id);

            //product.SetName(input.Name);
            //product.SetPrice(input.Price);
            //product.SetStockCount(input.StockCount);
            //product.SetImageName(input.ImageName);

            return ObjectMapper.Map<Book, BookDto>(null);
        }

        [Authorize(BookPermissions.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            //await _productRepository.DeleteAsync(id);
        }
    }
}
