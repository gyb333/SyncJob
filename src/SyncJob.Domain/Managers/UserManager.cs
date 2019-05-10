using Entitys;
using IManagers;
using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Managers
{
    public class UserManager:DomainService, IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            //Check.NotNullOrWhiteSpace(name, nameof(name));
    
            return await _userRepository.GetListAsync();
        }

        public  List<User> GetUsers()
        {
            //Check.NotNullOrWhiteSpace(name, nameof(name));

            return   _userRepository.GetUsers().ToList();
        }

    }
}
