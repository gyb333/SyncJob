using EntityFramework.Extensions.EFCore;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SyncJob.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Z.EntityFramework.Extensions;

namespace Repositories
{
    /// <summary>
    /// 方便自动注入Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TargetDbRepositoryBase<TEntity>: RepositoryBase<TargetDbContext, TEntity> 
      where TEntity : class, IEntity, new()
    {
        public TargetDbRepositoryBase(IDbContextProvider<TargetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
             
        }

        
    }


    public class TargetDbRepositoryBase<TEntity, TKey>: RepositoryBase<TargetDbContext, TEntity, TKey>
     where TEntity : class, IEntity<TKey>, new()
    {
        public TargetDbRepositoryBase(IDbContextProvider<TargetDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

       
    }

}
