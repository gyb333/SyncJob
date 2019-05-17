using Dapper;
using EntityFramework.Extensions.EFCore;
using EntityFrameworkCore;
using IRepository;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
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
using Z.Dapper.Plus;
using Z.EntityFramework.Extensions;
//using Z.EntityFramework.Extensions;

namespace Repositories
{
    public  class RepositoryBase<TDbContext,TEntity> : EfCoreRepository<TDbContext, TEntity>, IRepositoryBase<TEntity>
        where TDbContext : DbContext, IDbContextBase
        where TEntity : class, IEntity, new()
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


        public int ExecuteCommandDapper(string strSql, DynamicParameters param)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.Execute(strSql, param);
            }
        }

        public TEntity GetItem(string strSql, DynamicParameters param)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.GetItem<TEntity>(strSql, param);
            }

        }

        public IEnumerable<TEntity> GetItems(string strSql, DynamicParameters param)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.GetItems<TEntity>(strSql, param);
            }
        }



    }


    public class RepositoryBase<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>, IRepositoryBase<TEntity, TKey>
        where TDbContext : DbContext, IDbContextBase
        where TEntity : class, IEntity<TKey>, new()
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

        #region 批处理
        public async Task BatchDeleteAsync(IList<TEntity> entites)
        {

            await BulkBatchHelper.BatchDeleteAsync(DbContext, entites);
        }


        public async Task BatchInsertAsync(IList<TEntity> entites)
        {
            //await BulkBatchHelper.BatchInsertAsync(DbContext, entites);
            using (var conn = DbContext.CreatConnection())
            {
                try
                {
                    conn.BulkInsert(entites);

                }
                catch (Exception ex)
                {

                }

            }
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

        public   int ExecuteCommandDapper(string strSql, DynamicParameters param)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.Execute(strSql, param);
            }
        }

        public   TEntity GetItem(string strSql, DynamicParameters param)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.GetItem<TEntity>(strSql, param);
            }
             
        }

        public  IEnumerable<TEntity> GetItems(string strSql, DynamicParameters param)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.GetItems<TEntity>(strSql, param);
            }            
        }

        public   TEntity GetItemByKey(string strSql, TKey id)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.GetItemByKey<TEntity, TKey>(strSql, id);
            }
            
        }

        /// <summary>
        /// SQL 字符串长度存在限制，使用下面的分批次查询汇总
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetItemsByKeys(string strSql, TKey[] keys)
        {
            using (var conn = DbContext.CreatConnection())
            {
                return conn.GetItemsByKeys<TEntity, TKey>(strSql, keys);
            }
        }

        public IEnumerable<TEntity> GetItemsByKeys([NotNull]IEnumerable<IEntity<TKey>> entities, string sql, int PageSize=2000)
        {
            List<TEntity> result = new List<TEntity>();
            int pageCount = (entities.Count() - 1) / PageSize + 1;
            using (var dbConnection = DbContext.CreatConnection())
            {
                for (int index = 0; index < pageCount; index++)
                {
                    var keys = entities.Skip(PageSize * index).Take(PageSize).Select(p => p.Id).ToList();
                    var temp = dbConnection.GetItemsByKeys<TEntity, TKey>(sql, keys.ToArray());
                     result.AddRange(temp);  
                }
            }
            return result;
        }

        public IEnumerable<TEntity> GetItemsByTempTable([NotNull]IEnumerable<IEntity<TKey>> entities, string sql, string strTableName = "Ids")
        {
            IReadOnlyCollection<TEntity> result = null;
            using (var dbConnection = DbContext.CreatConnection())
            {
                switch (DbContext.GetDBType())
                {
                    case DBType.SQLServer:
                        //var conn = dbConnection as SqlConnection;
                        //result = conn.GetItemsByTempTable<TEntity, TKey>(entities, sql, strTableName);
                        //break;
                    case DBType.MySQL:
                        var mysqlConn = dbConnection as MySqlConnection;
                        result = mysqlConn.GetItemsByTempTable<TEntity, TKey>(entities, sql, strTableName);
                        break;
                    default:
                        break;
                }
                return result;
            }

        }

     
 
    }

}
