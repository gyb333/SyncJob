using SyncJob.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Repositories
{
    public class SourceDbRepositoryBase<TEntity>: EfCoreRepository<SourceDbContext, TEntity>
      where TEntity : class, IEntity
    {
        public SourceDbRepositoryBase(IDbContextProvider<SourceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }


    public class SourceDbRepositoryBase<TEntity, TKey>: EfCoreRepository<SourceDbContext, TEntity, TKey>
     where TEntity : class, IEntity<TKey>
    {
        public SourceDbRepositoryBase(IDbContextProvider<SourceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

}
