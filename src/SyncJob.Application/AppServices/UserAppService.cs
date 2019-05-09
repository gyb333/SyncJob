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
 
        private readonly IUserTargetRepository _userTargetRepository;

        public UserAppService(IUserRepository userReposity, IUserManager userManager, IUserTargetRepository userTargetRepository)
        {
            _userReposity = userReposity;
            _userManager = userManager;

            _userTargetRepository = userTargetRepository;

    }
        public async Task<List<User>> GetUsers()
        {
            return await _userManager.GetUsers();
        }


      

        public async Task ExecUser()
        {
            var users = await _userReposity.GetListAsync();
            var userList = ObjectMapper.Map<List<User>, List<UserTarget>>(users);
            await _userTargetRepository.BatchInsertAsync(userList);


        }


    }
}
