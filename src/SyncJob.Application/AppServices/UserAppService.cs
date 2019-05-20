using DTOs;
using Entitys;
using IAppServices;
using IManagers;
using IRepository;
using Microsoft.AspNetCore.Authorization;
using Permissions;
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
    [Authorize(UserPermissions.GroupName)]
    public class UserAppService : ApplicationService, IUserAppService
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

        public ValidatorOutput ValidatorUserInput(UserInputValidator input)
        {
            return new ValidatorOutput { Result = 42 };
        }

        public  List<User> GetUsersTest()
        {
            
            return   _userManager.GetUsers();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userManager.GetUsersAsync();
        }

        [Authorize(UserPermissions.Create)]
        public async Task ExecUser()
        {
             
            var users = await _userReposity.GetListAsync();
            var userList = ObjectMapper.Map<List<User>, List<UserTarget>>(users);
            //var keys=_userTargetRepository.GetItemsByKeys(userList,$"select UserID AS Id FROM Users where UserID in @ids");


            var keys = _userTargetRepository.GetItemsByTempTable(userList, $"select UserID AS Id,u.* FROM Users u");
            await _userTargetRepository.BatchInsertAsync(userList);


        }

        
    }
}
