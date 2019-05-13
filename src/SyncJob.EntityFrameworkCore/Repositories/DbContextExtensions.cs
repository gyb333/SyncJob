using EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repositories
{
    public static class DbContextExtensions
    {
        private static void CombineParams(DBType dBType,ref DbCommand command, params object[] parameters)
        {
            if (parameters != null)
            {
                if (dBType==DBType.SQLServer)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (!parameter.ParameterName.Contains("@"))
                            parameter.ParameterName = $"@{parameter.ParameterName}";
                        command.Parameters.Add(parameter);
                    }
                }else if (dBType == DBType.MySQL)
                {
                    foreach (MySqlParameter parameter in parameters)
                    {
                        if (!parameter.ParameterName.Contains("@"))
                            parameter.ParameterName = $"@{parameter.ParameterName}";
                        command.Parameters.Add(parameter);
                    }
                }

            }
        }

        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection dbConn, params object[] parameters)
        {
            DbConnection conn = facade.GetDbConnection();
            dbConn = conn;
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Open();
            DbCommand cmd = conn.CreateCommand();
            if (facade.ProviderName.ToUpper().Contains("SQLServer".ToUpper()))
            {
                cmd.CommandText = sql;
                CombineParams(DBType.SQLServer,ref cmd, parameters);
            }else if (facade.ProviderName.ToUpper().Contains("MySQL".ToUpper()))
            {
                cmd.CommandText = sql;
                CombineParams(DBType.MySQL, ref cmd, parameters);
            }
            return cmd;
        }

        public static DataTable SqlQuery(this DatabaseFacade facade, string sql, params object[] parameters)
        {
            DbConnection conn = null;
            DbDataReader reader = null;
            try
            {
                DbCommand cmd = CreateCommand(facade, sql, out conn, parameters);
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
        }
         

        public static List<T> ToList<T>(DbDataReader dr) where T : class, new()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties().ToList();
            while (dr.Read())
            {
                var obj = new T();

                foreach (var property in properties)
                {
                    try
                    {
                        //Oracle字段为大写
                        var id = dr.GetOrdinal(property.Name.ToUpper());
                        if (!dr.IsDBNull(id))
                        {
                            if (dr.GetValue(id) != DBNull.Value)
                            {
                                var val = dr.GetValue(id);
                                if (property.Name.ToUpper()== "IsValid".ToUpper())
                                {
                                    property.SetValue(obj, Convert.ToBoolean(val));
                                }
                                else
                                    property.SetValue(obj, val);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                    }
                }
                result.Add(obj);
            }
            return result;
        }
 

        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            DbConnection conn=null;
            DbDataReader reader=null;
            IEnumerable<T> result=null;
            try
            {
                DbCommand cmd = CreateCommand(facade, sql, out conn, parameters);
                reader = cmd.ExecuteReader();
                result = ToList<T>(reader);
               
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if(reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return result;


        }

        public static IEnumerable<T> ToEnumerable<T>(this DataTable dt) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            T[] ts = new T[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                    {
                        if (p.Name.ToUpper() == "IsValid".ToUpper())
                        {
                            p.SetValue(t, Convert.ToBoolean(row[p.Name]), null);
                        }
                        else
                            p.SetValue(t, row[p.Name], null);
                    }
                       
                }
                ts[i] = t;
                i++;
            }
            return ts;
        }
    }
}
