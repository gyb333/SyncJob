
using Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace IManagers
{
    public interface IUserManager: IDomainService
    {
        Task<List<User>> GetUsersAsync();

        List<User> GetUsers();
    }
}
