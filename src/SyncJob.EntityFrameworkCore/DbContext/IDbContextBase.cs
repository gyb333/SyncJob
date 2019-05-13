
using EntityFrameworkCore;
using Entitys;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EntityFrameworkCore
{
 
    public interface IDbContextBase : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

         
    

        string GetConnectionString();

        DBType GetDBType();

        IDbConnection CreatConnection();
    }
}