using EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EntityFrameworkCore
{
    public static class DapperExtensions
    {
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

    }
}
