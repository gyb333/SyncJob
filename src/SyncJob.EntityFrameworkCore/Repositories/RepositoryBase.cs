using Dapper;
using EntityFramework.Extensions.EFCore;
using EntityFrameworkCore;
using IRepository;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Z.EntityFramework.Extensions;

namespace Repositories
{
    public  class RepositoryBase<TDbContext,TEntity> : EfCoreRepository<TDbContext, TEntity>, IRepositoryBase<TEntity>
        where TDbContext : DbContext, IDbContextBase
        where TEntity : class, IEntity, new()
    {
        private DBType dbType = DBType.SQLServer;
        private string ConnectionString = string.Empty;


        public RepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            :base(dbContextProvider)
        {
            //ConnectionString = DbContext.Database.GetDbConnection().ConnectionString;
            //if (!DbContext.Database.ProviderName.ToUpper().Contains("SQLServer".ToUpper()))
            //{
            //    if (DbContext.Database.ProviderName.ToUpper().Contains("MySQL".ToUpper()))
            //    {
            //        dbType = DBType.MySQL;
            //    }
            //}
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

        #region 批处理
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
        #endregion

        #region 实体上下文执行命令
        public async Task<int> ExecuteSqlCommandAsync([NotNull] string sql, [NotNull] params object[] parameters)
        {
            return await DbContext.Database.ExecuteSqlCommandAsync(new RawSqlString(sql), parameters);
        }

        public async Task<int> ExecuteSqlCommandAsync([NotNull]  string sql, [NotNull] IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            return await DbContext.Database.ExecuteSqlCommandAsync(new RawSqlString(sql), parameters, cancellationToken);
        }


        public async Task<int> ExecuteSqlCommandAsync([NotNull] string  sql, CancellationToken cancellationToken = default)
        {
             return await DbContext.Database.ExecuteSqlCommandAsync(new RawSqlString(sql));
        }
        public async Task<int> ExecuteSqlCommandAsync([NotNull] FormattableString sql, CancellationToken cancellationToken = default)
        {
           
            return await DbContext.Database.ExecuteSqlCommandAsync(sql);
        }
        #endregion

        #region SQL查询 需要实体具有公共的无参构造函数
        public DataTable SqlQuery(string sql, params object[] parameters)
        {
            
            return  DbContext.Database.SqlQuery(sql, parameters);
        }

        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
            where T : class, new()
        {
            return DbContext.Database.SqlQuery<T>(sql, parameters);
        }

        public IEnumerable<TEntity> SqlQueryEntity(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TEntity>(sql, parameters);
        }
        #endregion


     

    }


    public class RepositoryBase<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>, IRepositoryBase<TEntity, TKey>
        where TDbContext : DbContext, IDbContextBase
        where TEntity : class, IEntity<TKey>, new()
    {


        private DBType dbType = DBType.SQLServer;
        private string ConnectionString = string.Empty;

        public RepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

            //ConnectionString = DbContext.Database.GetDbConnection().ConnectionString;
            //if (!DbContext.Database.ProviderName.ToUpper().Contains("SQLServer".ToUpper()))
            //{
            //    if (DbContext.Database.ProviderName.ToUpper().Contains("MySQL".ToUpper()))
            //    {
            //        dbType = DBType.MySQL;
            //    }
            //}
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

        #region 批处理
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
        #endregion

        #region 实体上下文执行命令
        public async Task<int> ExecuteSqlCommandAsync([NotNull] string sql, [NotNull] params object[] parameters)
        {
            return await DbContext.Database.ExecuteSqlCommandAsync(new RawSqlString(sql), parameters);
        }

        public async Task<int> ExecuteSqlCommandAsync([NotNull]  string sql, [NotNull] IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            return await DbContext.Database.ExecuteSqlCommandAsync(new RawSqlString(sql), parameters, cancellationToken);
        }


        public async Task<int> ExecuteSqlCommandAsync([NotNull] string sql, CancellationToken cancellationToken = default)
        {
            return await DbContext.Database.ExecuteSqlCommandAsync(new RawSqlString(sql));
        }
        public async Task<int> ExecuteSqlCommandAsync([NotNull] FormattableString sql, CancellationToken cancellationToken = default)
        {

            return await DbContext.Database.ExecuteSqlCommandAsync(sql);
        }
        #endregion

        #region SQL查询 需要实体具有公共的无参构造函数
        public DataTable SqlQuery(string sql, params SqlParameter[] parameters)
        {
            return DbContext.Database.SqlQuery(sql, parameters);
        }

        public IEnumerable<T> SqlQuery<T>(string sql, params SqlParameter[] parameters)
            where T : class, new()
        {
            return DbContext.Database.SqlQuery<T>(sql, parameters);
        }

        public IEnumerable<TEntity> SqlQueryEntity(string sql, params SqlParameter[] parameters)
        {
            return DbContext.Database.SqlQuery<TEntity>(sql, parameters);
        }
        #endregion


        public IEnumerable<TEntity> GetKeys([NotNull]IEnumerable<TEntity> entities, string sql,int PageSize=100)
        {
            IEnumerable<TEntity> result = new List<TEntity>();
            int pageCount = (entities.Count()-1) / PageSize+1;
            using (var dbConnection = DbContext.CreatConnection())
            {
                for (int index = 0; index < pageCount; index++)
                {
                    var keys = entities.Skip(PageSize * index).Take(PageSize).Select(p => p.Id).ToList();
                    //SqlParameter sqlParameter = new SqlParameter("@Keys", keys);
                    //var temp = SqlQueryEntity(sql, new SqlParameter[] { sqlParameter });
                    try
                    {
                        var temp = dbConnection.Query<TEntity>(sql, new
                        {
                            ids = keys.ToArray()
                        });
                        result.Union(temp);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
           



            return result;
        }

        
    }



}
