using EntityFramework.Extensions.EFCore;
using IRepository;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Z.EntityFramework.Extensions;

namespace Repositories
{
    public  class RepositoryBase<TDbContext,TEntity> : EfCoreRepository<TDbContext, TEntity>, IRepositoryBase<TEntity>
        where TDbContext : DbContext, IEfCoreDbContext
        where TEntity : class, IEntity
    {
        public RepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            :base(dbContextProvider)
        {
            // Using a constructor that requires optionsBuilder (EF Core) 
            EntityFrameworkManager.ContextFactory = context => DbContext;
            //EntityFrameworkManager.ContextFactory = context =>
            //{
            //    return dbContextProvider.GetDbContext();
            //};
            //EntityFrameworkManager.ContextFactory = context =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<TargetDbContext>();
            //    return new TargetDbContext(optionsBuilder.Options);
            //};
        }
        public async Task BatchDeleteAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchDeleteAsync(DbContext, entites);
        }


        public async Task BatchInsertAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchInsertAsync(DbContext, entites);
        }
    

        public async Task BatchMergeAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchMergeAsync(DbContext, entites);
        }

         

        public async Task BatchUpdateAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchUpdateAsync(DbContext, entites);
        }

        
    }


    public class RepositoryBase<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>, IRepositoryBase<TEntity, TKey>
        where TDbContext : DbContext, IEfCoreDbContext
        where TEntity : class, IEntity<TKey>
    {
        public RepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            // Using a constructor that requires optionsBuilder (EF Core) 
            EntityFrameworkManager.ContextFactory = context => DbContext;
            //EntityFrameworkManager.ContextFactory = context =>
            //{
            //    return dbContextProvider.GetDbContext();
            //};
            //EntityFrameworkManager.ContextFactory = context =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<TargetDbContext>();
            //    return new TargetDbContext(optionsBuilder.Options);
            //};
        }
        public async Task BatchDeleteAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchDeleteAsync(DbContext, entites);
        }


        public async Task BatchInsertAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchInsertAsync(DbContext, entites);
        }


        public async Task BatchMergeAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchMergeAsync(DbContext, entites);
        }



        public async Task BatchUpdateAsync(IList<TEntity> entites)
        {
            await BulkBatchHelper.BatchUpdateAsync(DbContext, entites);
        }


    }



}
