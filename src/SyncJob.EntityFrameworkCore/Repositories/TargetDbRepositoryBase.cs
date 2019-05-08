using SyncJob.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Repositories
{
    public class TargetDbRepositoryBase<TEntity>: EfCoreRepository<TargetDbContext, TEntity>
      where TEntity : class, IEntity
    {
        public TargetDbRepositoryBase(IDbContextProvider<TargetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


        
    }


    public class TargetDbRepositoryBase<TEntity, TKey>: EfCoreRepository<TargetDbContext, TEntity, TKey>
     where TEntity : class, IEntity<TKey>
    {
        public TargetDbRepositoryBase(IDbContextProvider<TargetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

}
