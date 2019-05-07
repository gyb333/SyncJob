using Entitys;
using IManagers;
using IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AppServices
{
    //ApplicationService提供了方便的日志记录和本地化功能
    public class UserAppService : ApplicationService, IApplicationService
    {
        private readonly IUserRepository _userReposity;
        private readonly IUserManager _userManager;

        private readonly IRepository<Book> _repositoryBook;


       public UserAppService(IUserRepository userReposity, IUserManager userManager, IRepository<Book> repositoryBook)
        {
            _userReposity = userReposity;
            _userManager = userManager;
            _repositoryBook = repositoryBook;

        }
        public async Task<List<User>> GetUsers()
        {
            return await _userManager.GetUsers();
        }


        public async Task<List<Book>> GetBooksDW()
        {
            return await _repositoryBook.GetListAsync();
        }
    }
}
