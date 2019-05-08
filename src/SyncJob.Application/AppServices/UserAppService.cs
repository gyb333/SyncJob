using Entitys;
using IManagers;
using IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace AppServices
{
    //ApplicationService提供了方便的日志记录和本地化功能
    public class UserAppService : ApplicationService, IApplicationService
    {
        private readonly IUserRepository _userReposity;
        private readonly IUserManager _userManager;
 
        private readonly IRepository<UserTarget> _repositoryUser;

        public UserAppService(IUserRepository userReposity, IUserManager userManager,  IRepository<UserTarget> repositoryUser)
        {
            _userReposity = userReposity;
            _userManager = userManager;
  
            _repositoryUser= repositoryUser;

    }
        public async Task<List<User>> GetUsers()
        {
            return await _userManager.GetUsers();
        }


      

        public async void ExecUser()
        {
            var users = await _userReposity.GetListAsync();
            var userList = ObjectMapper.Map<List<User>, List<UserTarget>>(users);

            
        }


    }
}
