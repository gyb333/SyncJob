using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;


namespace EntityFramework.Extensions.EFCore
{
    public static class BulkBatchHelper
    {

        public static async Task BatchInsertAsync<TDbContext, TEntity>(TDbContext DbContext, IList<TEntity> entities)
            where TDbContext : DbContext
            where TEntity : class, IEntity
        {
            Stopwatch watch = Stopwatch.StartNew();

            await DbContext.BulkInsertAsync(entities);
            await DbContext.BulkSaveChangesAsync();
            watch.Stop();
            Console.WriteLine(string.Format("{0} entities are created, cost {1} milliseconds.", entities.Count, watch.ElapsedMilliseconds));
        }

        public static async Task BatchUpdateAsync<TDbContext, TEntity>(TDbContext DbContext, IList<TEntity> entities)
             where TDbContext : DbContext
            where TEntity : class, IEntity
        {
            Stopwatch watch = Stopwatch.StartNew();
            await DbContext.BulkUpdateAsync(entities);
            await DbContext.BulkSaveChangesAsync();
            watch.Stop();
            Console.WriteLine(string.Format("{0} entities are updated, cost {1} milliseconds.", entities.Count, watch.ElapsedMilliseconds));
        }


        public static async Task BatchDeleteAsync<TDbContext, TEntity>(TDbContext DbContext, IList<TEntity> entities)
             where TDbContext : DbContext
            where TEntity : class, IEntity
        {
            Stopwatch watch = Stopwatch.StartNew();
            await DbContext.BulkDeleteAsync(entities);
            await DbContext.BulkSaveChangesAsync();
            watch.Stop();
            Console.WriteLine(string.Format("{0} entities are deleted, cost {1} milliseconds.", entities.Count, watch.ElapsedMilliseconds));
        }

        public static async Task BatchMergeAsync<TDbContext, TEntity>(TDbContext DbContext, IList<TEntity> entities)
             where TDbContext : DbContext
            where TEntity : class, IEntity
        {
            Stopwatch watch = Stopwatch.StartNew();
            await DbContext.BulkMergeAsync(entities);
            await DbContext.BulkSaveChangesAsync();
            watch.Stop();
            Console.WriteLine(string.Format("{0} entities are merged, cost {1} milliseconds.", entities.Count, watch.ElapsedMilliseconds));

        }

    }
}
