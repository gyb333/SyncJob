
using Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IRepository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> FindByUserCodeAsync(string userCode);
    }
}
