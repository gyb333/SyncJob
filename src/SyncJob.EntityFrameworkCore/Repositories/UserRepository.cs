using Entitys;
using IRepository;
using Microsoft.EntityFrameworkCore;
using SyncJob.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : SourceDbRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbContextProvider<SourceDbContext> dbContextProvider)
            :base(dbContextProvider)
        {

        }

        public async Task<User> FindByUserCodeAsync(string userCode)
        {
            return await DbContext.Set<User>()
                .Where(p => p.UserCode == userCode).FirstOrDefaultAsync();
        }

        public IEnumerable<User> GetUsers()
        { 
            return GetItems(
                $@"SELECT UserID,UserCode,CompanyID,CompanyBranchID,EmpID,PromoterID,IsValid,UserType,GroupPersonID,Remark
                    FROM user; ",null
                    );
        }


        

    }
}
