using Study.Common.EFCore;
using SyncJob.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities; 
using Volo.Abp.EntityFrameworkCore;

namespace Repositories
{
    public class SourceDbRepositoryBase<TEntity>: RepositoryBase<SourceDbContext, TEntity>
      where TEntity : class, IEntity, new()
    {
        public SourceDbRepositoryBase(IDbContextProvider<SourceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }


    public class SourceDbRepositoryBase<TEntity, TKey>: RepositoryBase<SourceDbContext, TEntity, TKey>
     where TEntity : class, IEntity<TKey>, new()
    {
        public SourceDbRepositoryBase(IDbContextProvider<SourceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

}
