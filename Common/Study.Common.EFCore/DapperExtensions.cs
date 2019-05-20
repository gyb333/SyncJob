using Dapper;

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Entities;
using Z.Dapper.Plus;

namespace Study.Common.EFCore
{
    public static class DapperExtensions
    {
        #region 获取数据库连接
        public static IDbConnection CreatDbConnectionDapper(this DbContext facade,DBType dbType,string Connection)
        {
            IDbConnection dbConnection = null;
            switch (dbType)
            {
                case DBType.MySQL:
                    if (dbConnection == null)
                    {
                        dbConnection = new MySqlConnection(Connection);
                    }
                    //判断连接状态
                    if (dbConnection.State == ConnectionState.Closed)
                    {
                        dbConnection.Open();
                    }
                    break;

                case DBType.SQLServer:
                    if (dbConnection == null)
                    {
                        dbConnection = new SqlConnection(Connection);
                    }
                    //判断连接状态
                    if (dbConnection.State == ConnectionState.Closed)
                    {
                        dbConnection.Open();
                    }
                    break;
                default:
                    break;
            }
            return dbConnection;
        }
        #endregion

         


        public static int ExecuteCommandDapper(this IDbConnection conn, string strSql, DynamicParameters param)
        {
            return conn.Execute(strSql, param);
        }


        public static TEntity GetItem<TEntity>(this IDbConnection conn, string strSql, DynamicParameters param)
        {
            return conn.QueryFirstOrDefault<TEntity>(strSql, param);
        }

        public static IEnumerable<TEntity> GetItems<TEntity>(this IDbConnection conn, string strSql, DynamicParameters param)
        {
            return conn.Query<TEntity>(strSql, param);
        }

        public static TEntity GetItemByKey<TEntity,TKey>(this IDbConnection conn, string strSql, TKey id)
            where TEntity : class, IEntity<TKey>
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            return conn.QueryFirstOrDefault<TEntity>(strSql, param);
        }

        public static IEnumerable<TEntity> GetItemsByKeys<TEntity, TKey>(this IDbConnection conn, string strSql, TKey[] keys)
            where TEntity : class, IEntity<TKey>
        {
            return conn.Query<TEntity>(strSql, new
            {
                ids = keys
            });
        }

        public static TEntity GetItemByKey<TEntity>(this IDbConnection conn, string strSql, [NotNull]TEntity entity)
            where TEntity : class, IEntity
        {
            DynamicParameters param = new DynamicParameters();
            var keys = entity.GetKeys();
            for (int i=0;i<keys.Length;i++)
            {
                param.Add("@Key"+i, keys[i]);
            }
            return conn.QueryFirstOrDefault<TEntity>(strSql, param);
        }



        public static IReadOnlyCollection<TEntity> GetItemsByTempTable<TEntity, TKey>(this SqlConnection conn,[NotNull]IEnumerable<IEntity<TKey>> entities, string sql, string strTableName = "Ids")
             where TEntity : class, IEntity<TKey>
        {
            var itemList = new HashSet<IEntity<TKey>>(entities);
            if (itemList.Count == 0) { return Enumerable.Empty<TEntity>().ToList().AsReadOnly(); }

            var itemDataTable = new DataTable();
            itemDataTable.Columns.Add("Id", typeof(TKey));
            foreach(var each in itemList)
            {
                itemDataTable.Rows.Add(each.Id);
            }
            //itemList.ForEach(itemid => itemDataTable.Rows.Add(itemid));

            using (var transaction = conn.BeginTransaction())
            {
                conn.Execute(
                   $"CREATE TABLE #Temp{strTableName} (Id int NOT NULL PRIMARY KEY CLUSTERED);",
                   transaction: transaction
                );

                new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction)
                {
                    DestinationTableName = $"#Temp{strTableName}",
                    BulkCopyTimeout = 3600 // ridiculously large
                }
                   .WriteToServer(itemDataTable);
                string strSQL = $@"SELECT d.* FROM ({sql}) d 
            INNER JOIN #Temp{strTableName} i ON d.Id = i.Id; 
            DROP TABLE #Temp{strTableName};";
                var result = conn.Query<TEntity>(strSQL, transaction: transaction,commandTimeout: 3600)
                   .ToList() .AsReadOnly();
                transaction.Rollback(); // Or commit if you like
                return result;
            }
        }

        #region DataTableToCsv
        ///将DataTable转换为标准的CSV
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>返回标准的CSV</returns>
        private static string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        #endregion

        public static IReadOnlyCollection<TEntity> GetItemsByTempTable<TEntity, TKey>(this MySqlConnection conn, [NotNull]IEnumerable<IEntity<TKey>> entities, string sql,string strTableName="Ids")
            where TEntity : class, IEntity<TKey> 
        {
            var keys = entities.Select(p=>new IdEntity<TKey>(p.Id)).ToArray(); 


            var itemList = new HashSet<IdEntity<TKey>>(keys);
            if (itemList.Count == 0) { return Enumerable.Empty<TEntity>().ToList().AsReadOnly(); }
           
            using (var transaction = conn.BeginTransaction())
            {
                //不能建TEMPORARY 临时表
                string strTempSQL = $" CREATE  TABLE IF NOT EXISTS `#Temp{strTableName}` (Id {TypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(typeof(TKey))} NOT NULL PRIMARY KEY  );";
                conn.Execute(strTempSQL, transaction: transaction);

                try { conn.BulkInsert(itemList.ToArray());
                }catch(Exception ex)
                {

                }
                
                string strSQL = $@"SELECT d.* FROM ({sql}) d 
            INNER JOIN `#Temp{strTableName}` i ON d.Id = i.Id; 
            DROP TABLE IF EXISTS `#Temp{strTableName}`;";
                var result = conn.Query<TEntity>(strSQL,
                    transaction: transaction, commandTimeout: 3600)
                   .ToList().AsReadOnly();
            transaction.Rollback(); // Or commit if you like
            return result;
        }
    }

        public static int ExecuteStoredProcedureDapper(this IDbConnection conn, string spName, DynamicParameters param,
            string errMsgParamName = null)
        {
            var result = conn.Execute(spName, param, commandType: CommandType.StoredProcedure);
            if (errMsgParamName != null)
            {
                var errMsg = param.Get<string>(errMsgParamName);
                if (!string.IsNullOrEmpty(errMsg)) throw new Exception(errMsg);
            }
            return result;
        }




        #region 获取BillNo(新)
        /// <summary>
        ///     生成编号BillNo
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="BillTypeID"></param>
        /// <param name="CompanyID"></param>
        /// <param name="DistributorID"></param>
        /// <param name="CompBranchID"></param>
        /// <returns></returns>
        private static string GenerateBillNo(IDbConnection conn, int intBillTypeCode, int CompanyID, int DistributorID,
            int CompBranchID)
        {
            var strRes = string.Empty;
            try
            {
                var param = new DynamicParameters();
                param.Add("@var_intBillTypeCode", intBillTypeCode);
                param.Add("@var_intCompanyID", CompanyID);
                param.Add("@var_intDistributorID", DistributorID);
                param.Add("@var_intCompBranchID", CompBranchID);
                param.Add("@var_chvBillNo", dbType: DbType.String, direction: ParameterDirection.Output);

                var strSQL = "prGetBillNo";
                ExecuteStoredProcedureDapper(conn, strSQL, param);
                strRes = param.Get<string>("@var_chvBillNo");
            }
            catch (Exception ex)
            {
                var strMessage = "执行存储过程prGetBillNo异常";
                throw new Exception(strMessage, ex);
            }

            return strRes;
        }

        #endregion


    }




}
