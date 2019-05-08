using Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Managers
{
    public class UserTargetManager : DomainService
    {
        private readonly IRepository<UserTarget> _userRepository;
        public UserTargetManager(IRepository<UserTarget> userRepository)
        {
            _userRepository=userRepository;
        }


        

    }
}
