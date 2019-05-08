using EntityFramework.Extensions.EFCore;
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
    public class UserTargetRepository : EfCoreRepository<TargetDbContext, UserTarget>, IUserTargetRepository
    {
        public UserTargetRepository(IDbContextProvider<TargetDbContext> dbContextProvider)
            :base(dbContextProvider)
        {

        }

        public async Task BatchInsert(IList<UserTarget> entites)
        {
           await BulkBatchHelper.BatchInsertAsync(DbContext, entites);
        }
    }
}
